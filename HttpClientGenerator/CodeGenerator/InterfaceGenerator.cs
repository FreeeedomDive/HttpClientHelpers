using System.Text;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal class InterfaceGenerator : IInterfaceGenerator
{
    public GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo, GeneratorOptions options)
    {
        var interfaceName = apiControllerInfo.GetClientName(true);
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine("using System.Threading.Tasks;")
                 .AppendLine()
                 .AppendLine($"namespace {options.ClientNamespace}.{apiControllerInfo.Name};")
                 .AppendLine()
                 .AppendLine($"public interface {interfaceName}")
                 .AppendLine("{");
        foreach (var method in apiControllerInfo.Methods)
        {
            const string task = "Task";
            var friendlyTypeName = method.ReturnType.GetFriendlyTypeName(method.IsReturnTypeNullable);
            var taskWrapperTypeName = string.IsNullOrEmpty(friendlyTypeName) ? task : $"{task}<{friendlyTypeName}>";
            sb.AppendIndent().Append($"{taskWrapperTypeName} {method.Name}Async(");
            var parameters = method
                             .Parameters
                             .Select(x => $"{x.Type.GetFriendlyTypeName(x.IsNullable)} {x.Name}{(string.IsNullOrEmpty(x.OptionalValue) ? string.Empty : $" = {x.OptionalValue}")}")
                             .ToArray();
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