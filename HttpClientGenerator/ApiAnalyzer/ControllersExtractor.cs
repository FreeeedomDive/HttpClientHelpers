using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;

internal class ControllersExtractor : IControllersExtractor
{
    public ControllersExtractor(IControllerMethodsExtractor controllerMethodsExtractor)
    {
        this.controllerMethodsExtractor = controllerMethodsExtractor;
    }

    public ApiControllerInfo[] ExtractAllFromType<TController>()
    {
        var assembly = typeof(TController).Assembly;
        var controllers = GetAllControllersFromAssembly(assembly);
        return controllers.Select(GetApiControllerInfo).Where(x => x is not null).Select(x => x!).ToArray();
    }

    private static Type[] GetAllControllersFromAssembly(Assembly assembly)
    {
        return assembly
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(ControllerBase)) && !t.IsAbstract)
               .ToArray();
    }

    private ApiControllerInfo? GetApiControllerInfo(Type controllerType)
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
            RouteTemplate = routeMethodAttribute?.Template ?? string.Empty,
            Methods = methods,
        };
    }

    private readonly IControllerMethodsExtractor controllerMethodsExtractor;
}