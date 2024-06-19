using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface ICommonClientGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos, string apiProjectName, GeneratorOptions options);
}