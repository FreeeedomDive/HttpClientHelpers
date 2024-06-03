using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

public static class InterfaceGenerator
{
    public static GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo)
    {
        var interfaceName = apiControllerInfo.GetClientName(true);
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine($"namespace {apiControllerInfo.Namespace}.Client;")
                 .AppendLine()
                 .AppendLine($"public interface {interfaceName}")
                 .AppendLine("{");
        foreach (var method in apiControllerInfo.Methods)
        {
            const string task = "System.Threading.Tasks.Task";
            var friendlyTypeName = method.ReturnType.GetFriendlyTypeName();
            var taskWrapperTypeName = string.IsNullOrEmpty(friendlyTypeName) ? task : $"{task}<{friendlyTypeName}>";
            sb.AppendIndent().Append($"{taskWrapperTypeName} {method.Name}Async(");
            var parameters = method.Parameters.Select(x => $"{x.Type.GetFriendlyTypeName()} {x.Name}");
            sb.Append(string.Join(", ", parameters));
            sb.AppendLine(");");
        }

        sb.AppendLine("}");
        return new GeneratedFileContent
        {
            FileName = $"{interfaceName}.cs",
            FolderName = apiControllerInfo.Name,
            FileContent = sb.ToString(),
        };
    }
}