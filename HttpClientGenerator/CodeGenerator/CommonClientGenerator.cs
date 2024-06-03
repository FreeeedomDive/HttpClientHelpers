using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

public static class CommonClientGenerator
{
    public static GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos)
    {
        var commonNamespace = apiControllerInfos.First().Namespace;
        var clientName = $"{commonNamespace.Replace(".", string.Empty)}Client";
        var sb = new StringBuilder()
            .AppendLine("/* Generated file */")
            .AppendLine();
        foreach (var apiControllerInfo in apiControllerInfos)
        {
            sb.AppendLine($"using {commonNamespace}.Client.{apiControllerInfo.Name}");
        }

        sb.AppendLine($"namespace {commonNamespace}.Client;")
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