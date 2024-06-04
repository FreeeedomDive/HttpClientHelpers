using System.Text;
using System.Text.RegularExpressions;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal static class ClientGenerator
{
    public static GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo)
    {
        var className = apiControllerInfo.GetClientName();
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine("using RestSharp;")
                 .AppendLine($"using {apiControllerInfo.Namespace}.Client.Extensions;")
                 .AppendLine()
                 .AppendLine($"namespace {apiControllerInfo.Namespace}.Client.{apiControllerInfo.Name};")
                 .AppendLine()
                 .AppendLine($"public class {className} : {apiControllerInfo.GetClientName(true)}")
                 .AppendLine("{")
                 .AppendIndent().AppendLine($"public {className}(RestSharp.RestClient restClient)")
                 .AppendIndent().AppendLine("{")
                 .AppendIndent(2).AppendLine("this.restClient = restClient;")
                 .AppendIndent().AppendLine("}")
                 .AppendLine();

        foreach (var method in apiControllerInfo.Methods)
        {
            const string task = "System.Threading.Tasks.Task";
            var friendlyTypeName = method.ReturnType.GetFriendlyTypeName();
            var isVoidReturn = string.IsNullOrEmpty(friendlyTypeName);
            var taskWrapperTypeName = isVoidReturn ? task : $"{task}<{friendlyTypeName}>";
            sb.AppendIndent().Append($"public async {taskWrapperTypeName} {method.Name}Async(");
            var parameters = method
                             .Parameters
                             .Select(x => $"{x.Type.GetFriendlyTypeName()} {x.Name}{(string.IsNullOrEmpty(x.OptionalValue) ? string.Empty : $" = {x.OptionalValue}")}")
                             .ToArray();
            sb.Append(string.Join(", ", parameters))
              .AppendLine(")")
              .AppendIndent().AppendLine("{")
              .AppendIndent(2)
              .Append("var request = new RestRequest(\"")
              .Append($"{RemoveTypeConstraintsFromRoute(apiControllerInfo.RouteTemplate)}/{RemoveTypeConstraintsFromRoute(method.RouteTemplate)}")
              .AppendLine($"\", Method.{method.HttpMethod[0] + method.HttpMethod[1..].ToLower()});");
            foreach (var parameter in method.Parameters)
            {
                switch (parameter.Source)
                {
                    case ParameterSource.Route:
                        sb.AppendIndent(2).AppendLine($"request.AddUrlSegment(\"{parameter.Name}\", {parameter.Name});");
                        break;
                    case ParameterSource.Query:
                        sb.AppendIndent(2).AppendLine($"request.AddQueryParameter(\"{parameter.Name}\", {parameter.Name}.ToString());");
                        break;
                    case ParameterSource.Body:
                        sb.AppendIndent(2).AppendLine($"request.AddJsonBody({parameter.Name});");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            sb.AppendIndent(2).AppendLine("var response = await restClient.ExecuteAsync(request);");
            if (isVoidReturn)
            {
                sb.AppendIndent(2).AppendLine("response.ThrowIfNotSuccessful();");
            }
            else
            {
                sb.AppendIndent(2).AppendLine($"return response.TryDeserialize<{method.ReturnType.GetFriendlyTypeName()}>();");
            }

            sb.AppendIndent().AppendLine("}").AppendLine();
        }

        sb.AppendIndent().AppendLine("private readonly RestSharp.RestClient restClient;")
          .AppendLine("}");
        return new GeneratedFileContent
        {
            FileName = $"{className}.cs",
            FolderName = apiControllerInfo.Name,
            FileContent = sb.ToString(),
        };
    }

    private static string RemoveTypeConstraintsFromRoute(string route)
    {
        return Regex.Replace(route, @":\w+", "");
    }
}