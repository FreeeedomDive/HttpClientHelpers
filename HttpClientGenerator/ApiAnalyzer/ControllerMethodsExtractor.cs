using System.Reflection;
using ClientsGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace HttpClientGenerator.ApiAnalyzer;

public static class ControllerMethodsExtractor
{
    public static ApiMethodInfo[] Extract(Type controllerType)
    {
        var methods = GetApiMethodsFromControllers(controllerType);
        return methods.Length == 0
            ? Array.Empty<ApiMethodInfo>()
            : methods.SelectMany(CollectApiMethodInfo).ToArray();
    }

    private static MethodInfo[] GetApiMethodsFromControllers(Type controllerType)
    {
        return controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                             .Where(method => method.GetCustomAttributes().Any(attr => attr is HttpMethodAttribute))
                             .ToArray();
    }

    private static ApiMethodInfo[] CollectApiMethodInfo(MethodInfo method)
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
                return new ApiParameterInfo
                {
                    Name = parameterInfo.Name ?? "",
                    Type = GetInnerType(parameterInfo.ParameterType),
                    Source = attributesTypes.Contains(typeof(FromRouteAttribute))
                        ? ParameterSource.Route
                        : attributesTypes.Contains(typeof(FromQueryAttribute))
                            ? ParameterSource.Query
                            : attributesTypes.Contains(typeof(FromBodyAttribute))
                                ? ParameterSource.Body
                                : throw new ArgumentOutOfRangeException(nameof(parameterInfo), "Parameter should contain one of [From*] attributes"),
                };
            }
        ).ToArray();
        return httpMethodAttribute.HttpMethods.Select(
            httpMethod => new ApiMethodInfo
            {
                Name = method.Name,
                HttpMethod = httpMethod,
                RouteTemplate = httpMethodAttribute.Template ?? routeMethodAttribute?.Template ?? string.Empty,
                Parameters = parameters,
                ReturnType = GetInnerType(method.ReturnType),
            }
        ).ToArray();
    }

    private static Type GetInnerType(Type type)
    {
        // Handle Task<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
        {
            type = type.GetGenericArguments()[0];
        }

        // Handle Task
        else if (type == typeof(Task))
        {
            return typeof(void);
        }

        // Handle ActionResult<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ActionResult<>))
        {
            type = type.GetGenericArguments()[0];
        }

        // Handle ActionResult
        else if (type == typeof(ActionResult))
        {
            return typeof(void);
        }

        // If the resulting type is still a Task or ActionResult, recursively unwrap it
        if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Task<>) || type.GetGenericTypeDefinition() == typeof(ActionResult<>)))
        {
            return GetInnerType(type);
        }

        return type;
    }
}