using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;

internal static class ControllerMethodsExtractor
{
    public static ApiMethodInfo[] Extract(Type controllerType)
    {
        var methods = GetApiMethodsFromControllers(controllerType);
        return methods.Length == 0
            ? Array.Empty<ApiMethodInfo>()
            : methods.SelectMany(x => CollectApiMethodInfo(controllerType, x)).ToArray();
    }

    private static MethodInfo[] GetApiMethodsFromControllers(Type controllerType)
    {
        return controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                             .Where(method => method.GetCustomAttributes().Any(attr => attr is HttpMethodAttribute))
                             .ToArray();
    }

    private static ApiMethodInfo[] CollectApiMethodInfo(Type controllerType, MethodInfo method)
    {
        var httpMethodAttribute = method.GetCustomAttribute<HttpMethodAttribute>();
        if (httpMethodAttribute is null)
        {
            return Array.Empty<ApiMethodInfo>();
        }

        var routeMethodAttribute = method.GetCustomAttribute<RouteAttribute>();
        var parameters = method.GetParameters().Select(
            parameterInfo =>
            {
                var attributesTypes = parameterInfo.CustomAttributes.Select(attr => attr.AttributeType).ToHashSet();
                var parameterName = parameterInfo.Name ?? string.Empty;
                return new ApiParameterInfo
                {
                    Name = parameterName,
                    Type = GetInnerType(parameterInfo.ParameterType),
                    IsNullable = IsNullable(parameterInfo),
                    Source = attributesTypes.Contains(typeof(FromRouteAttribute))
                        ? ParameterSource.Route
                        : attributesTypes.Contains(typeof(FromQueryAttribute))
                            ? ParameterSource.Query
                            : attributesTypes.Contains(typeof(FromBodyAttribute))
                                ? ParameterSource.Body
                                : throw new ArgumentOutOfRangeException(
                                    parameterName,
                                    $"Parameter should contain one of [From*] attributes, method: {controllerType.Name}.{method.Name}"
                                ),
                    OptionalValue = parameterInfo.IsOptional ? parameterInfo.DefaultValue is null ? "null" : parameterInfo.DefaultValue.ToString() : null,
                };
            }
        ).ToArray();
        return httpMethodAttribute.HttpMethods.Select(
            httpMethod => new ApiMethodInfo
            {
                Name = method.Name.Replace("Async", string.Empty),
                HttpMethod = httpMethod,
                RouteTemplate = httpMethodAttribute.Template ?? routeMethodAttribute?.Template ?? string.Empty,
                Parameters = parameters,
                ReturnType = GetInnerType(method.ReturnType),
                IsReturnTypeNullable = IsNullable(method.ReturnParameter),
            }
        ).ToArray();
    }

    private static Type GetInnerType(Type type)
    {
        // Handle Task<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
        {
            return GetInnerType(type.GetGenericArguments()[0]);
        }

        // Handle Task
        else if (type == typeof(Task))
        {
            return typeof(void);
        }

        // Handle ActionResult<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ActionResult<>))
        {
            return GetInnerType(type.GetGenericArguments()[0]);
        }

        // Handle ActionResult
        else if (type == typeof(ActionResult))
        {
            return typeof(void);
        }

        // Handle Nullable<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return GetInnerType(type.GetGenericArguments()[0]);
        }

        // If the resulting type is still a Task or ActionResult, recursively unwrap it
        if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Task<>) || type.GetGenericTypeDefinition() == typeof(ActionResult<>)))
        {
            return GetInnerType(type);
        }

        return type;
    }

    private static bool IsNullable(ParameterInfo p)
    {
        return new NullabilityInfoContext().Create(p).WriteState is NullabilityState.Nullable;
    }
}