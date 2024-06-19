using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;

internal interface IControllersExtractor
{
    ApiControllerInfo[] ExtractAllFromType<TController>(GeneratorOptions options);
}