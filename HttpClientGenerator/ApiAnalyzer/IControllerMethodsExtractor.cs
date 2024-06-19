using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;

internal interface IControllerMethodsExtractor
{
    ApiMethodInfo[] Extract(Type controllerType);
}