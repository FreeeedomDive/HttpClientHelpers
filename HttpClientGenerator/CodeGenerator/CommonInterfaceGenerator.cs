using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal class CommonInterfaceGenerator : ICommonInterfaceGenerator
{
    public GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos, string apiProjectName, GeneratorOptions options)
    {
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
          .AppendLine().AppendLine($"public interface {interfaceName}")
          .AppendLine("{");
        foreach (var apiControllerInfo in apiControllerInfos)
        {
            sb.AppendIndent().AppendLine($"{apiControllerInfo.GetClientName(true)} {apiControllerInfo.Name} {{ get; }}");
        }

        sb.AppendLine("}");
        return new GeneratedFileContent
        {
            FileName = $"{interfaceName}.cs",
            FolderName = null,
            FileContent = sb.ToString(),
        };
    }
}