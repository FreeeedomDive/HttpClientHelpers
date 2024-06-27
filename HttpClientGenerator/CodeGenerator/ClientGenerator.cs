using System.Text;
using System.Text.RegularExpressions;
using Xdd.HttpHelpers.HttpClientGenerator.Extensions;
using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal class ClientGenerator : IClientGenerator
{
    public GeneratedFileContent Generate(ApiControllerInfo apiControllerInfo, GeneratorOptions options)
    {
        var className = apiControllerInfo.GetClientName();
        var sb = new StringBuilder()
                 .AppendLine("/* Generated file */")
                 .AppendLine("using System.Threading.Tasks;")
                 .AppendLine()
                 .AppendLine("using Xdd.HttpHelpers.Models.Extensions;")
                 .AppendLine("using Xdd.HttpHelpers.Models.Requests;")
                 .AppendLine()
                 .AppendLine($"namespace {options.ClientNamespace}.{apiControllerInfo.Name};")
                 .AppendLine()
                 .AppendLine($"public class {className} : {apiControllerInfo.GetClientName(true)}")
                 .AppendLine("{")
                 .AppendIndent().AppendLine($"public {className}(RestSharp.RestClient client)")
                 .AppendIndent().AppendLine("{")
                 .AppendIndent(2).AppendLine("this.client = client;")
                 .AppendIndent().AppendLine("}")
                 .AppendLine();

        foreach (var method in apiControllerInfo.Methods)
        {
            const string task = "Task";
            var friendlyTypeName = method.ReturnType.GetFriendlyTypeName(method.IsReturnTypeNullable);
            var isVoidReturn = string.IsNullOrEmpty(friendlyTypeName);
            var taskWrapperTypeName = isVoidReturn ? task : $"{task}<{friendlyTypeName}>";
            sb.AppendIndent().Append($"public async {taskWrapperTypeName} {method.Name}Async(");
            var parameters = method
                             .Parameters
                             .Select(x => $"{x.Type.GetFriendlyTypeName(x.IsNullable)} {x.Name}{(string.IsNullOrEmpty(x.OptionalValue) ? string.Empty : $" = {x.OptionalValue}")}")
                             .ToArray();
            sb.Append(string.Join(", ", parameters))
              .AppendLine(")")
              .AppendIndent().AppendLine("{")
              .AppendIndent(2)
              .Append("var requestBuilder = new RequestBuilder($\"")
              .Append($"{RemoveTypeConstraintsFromRoute(apiControllerInfo.RouteTemplate)}/{RemoveTypeConstraintsFromRoute(method.RouteTemplate)}")
              .AppendLine($"\", HttpRequestMethod.{method.HttpMethod.ToUpper()});");
            foreach (var parameter in method.Parameters)
            {
                switch (parameter.Source)
                {
                    case ParameterSource.Query:
                        sb.AppendIndent(2).AppendLine($"requestBuilder.WithQueryParameter(\"{parameter.Name}\", {parameter.Name});");
                        break;
                    case ParameterSource.Body:
                        sb.AppendIndent(2).AppendLine($"requestBuilder.WithJsonBody({parameter.Name});");
                        break;
                }
            }

            if (isVoidReturn)
            {
                sb.AppendIndent(2).AppendLine("await client.MakeRequestAsync(requestBuilder.Build());");
            }
            else
            {
                sb.AppendIndent(2).AppendLine($"return await client.MakeRequestAsync<{method.ReturnType.GetFriendlyTypeName(method.IsReturnTypeNullable)}>(requestBuilder.Build());");
            }

            sb.AppendIndent().AppendLine("}").AppendLine();
        }

        sb.AppendIndent().AppendLine("private readonly RestSharp.RestClient client;")
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