using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal class CommonClientGenerator : ICommonClientGenerator
{
    public GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos, string apiProjectName, GeneratorOptions options)
    {
        var clientName = options.ClientName ?? $"{apiProjectName.Replace(".", string.Empty)}Client";
        var interfaceName = options.InterfaceName ?? $"I{apiProjectName.Replace(".", string.Empty)}Client";
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine();
        foreach (var apiControllerInfo in apiControllerInfos)
        {
            sb.AppendLine($"using {options.ClientNamespace}.{apiControllerInfo.Name};");
        }

        sb.AppendLine()
          .AppendLine($"namespace {options.ClientNamespace};")
          .AppendLine()
          .AppendLine($"public class {clientName} : {interfaceName}")
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