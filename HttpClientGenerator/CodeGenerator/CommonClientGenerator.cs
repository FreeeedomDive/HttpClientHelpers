using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal class CommonClientGenerator : ICommonClientGenerator
{
    public GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos, GeneratorOptions options)
    {
        var commonNamespace = apiControllerInfos.First().Namespace;
        var clientName = options.ClientName ?? $"{commonNamespace.Replace(".", string.Empty)}Client";
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine();
        foreach (var apiControllerInfo in apiControllerInfos)
        {
            sb.AppendLine($"using {commonNamespace}.Client.{apiControllerInfo.Name};");
        }

        sb.AppendLine()
          .AppendLine($"namespace {commonNamespace}.Client;")
          .AppendLine()
          .AppendLine($"public class {clientName} : I{clientName}")
          .AppendLine("{")
          .AppendIndent().AppendLine($"public {clientName}(RestSharp.RestClient restClient)")
          .AppendIndent().AppendLine("{");

        foreach (var apiControllerInfo in apiControllerInfos)
        {
            sb.AppendIndent(2).AppendLine($"{apiControllerInfo.Name} = new {apiControllerInfo.GetClientName()}(restClient);");
        }

        sb.AppendIndent().AppendLine("}").AppendLine();

        foreach (var apiControllerInfo in apiControllerInfos)
        {
            sb.AppendIndent().AppendLine($"public {apiControllerInfo.GetClientName(true)} {apiControllerInfo.Name} {{ get; }}");
        }

        sb.AppendLine("}");
        return new GeneratedFileContent
        {
            FileName = $"{clientName}.cs",
            FolderName = null,
            FileContent = sb.ToString(),
        };
    }
}