using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface ICommonInterfaceGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos);
}