using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

public static class CommonInterfaceGenerator
{
    public static GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos)
    {
        var commonNamespace = apiControllerInfos.First().Namespace;
        var interfaceName = $"I{commonNamespace.Replace(".", string.Empty)}Client";
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine($"namespace {commonNamespace}.Client;")
                 .AppendLine()
                 .AppendLine($"public interface {interfaceName}")
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