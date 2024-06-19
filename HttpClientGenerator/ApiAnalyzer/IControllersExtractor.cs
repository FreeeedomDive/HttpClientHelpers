using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;

internal interface IControllersExtractor
{
    ApiControllerInfo[] ExtractAllFromType<TController>();
}