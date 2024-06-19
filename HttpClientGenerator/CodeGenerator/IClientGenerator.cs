using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface IClientGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo, GeneratorOptions options);
}