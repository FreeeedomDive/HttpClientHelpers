using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface IClientGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo);
}