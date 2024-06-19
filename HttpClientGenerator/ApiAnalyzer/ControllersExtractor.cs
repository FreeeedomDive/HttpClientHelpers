using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;

internal class ControllersExtractor : IControllersExtractor
{
    public ControllersExtractor(IControllerMethodsExtractor controllerMethodsExtractor)
    {
        this.controllerMethodsExtractor = controllerMethodsExtractor;
    }

    public ApiControllerInfo[] ExtractAllFromType<TController>(GeneratorOptions options)
    {
        var assembly = typeof(TController).Assembly;
        var @namespace = options.ClientNamespace ?? assembly.GetName().Name ?? string.Empty;
        var controllers = GetAllControllersFromAssembly(assembly);
        return controllers.Select(x => GetApiControllerInfo(x, @namespace)).Where(x => x is not null).Select(x => x!).ToArray();
    }

    private static Type[] GetAllControllersFromAssembly(Assembly assembly)
    {
        return assembly
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(ControllerBase)) && !t.IsAbstract)
               .ToArray();
    }

    private ApiControllerInfo? GetApiControllerInfo(Type controllerType, string commonNamespace)
    {
        var methods = controllerMethodsExtractor.Extract(controllerType);
        if (methods.Length == 0)
        {
            return null;
        }

        var routeMethodAttribute = controllerType.GetCustomAttribute<RouteAttribute>();
        return new ApiControllerInfo
        {
            Name = controllerType.Name.Replace("Controller", string.Empty),
            Namespace = commonNamespace,
            RouteTemplate = routeMethodAttribute?.Template ?? string.Empty,
            Methods = methods,
        };
    }

    private readonly IControllerMethodsExtractor controllerMethodsExtractor;
}