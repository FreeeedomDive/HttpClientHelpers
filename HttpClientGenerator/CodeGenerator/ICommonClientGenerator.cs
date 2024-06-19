using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface ICommonClientGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos);
}