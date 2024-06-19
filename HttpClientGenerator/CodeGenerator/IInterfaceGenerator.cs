using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface IInterfaceGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo);
}