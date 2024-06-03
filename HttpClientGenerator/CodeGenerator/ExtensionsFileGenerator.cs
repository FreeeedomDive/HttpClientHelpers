using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

public static class ExtensionsFileGenerator
{
    public static GeneratedFileContent GenerateRestResponseExtensions(ApiControllerInfo apiControllerInfo)
    {
        var restResponseExtensionsFileContent = @$"/* Generated file */
using Newtonsoft.Json;
using RestSharp;
using Xdd.HttpHelpers.Models.Exceptions;

namespace {apiControllerInfo.Namespace}.Client.Extensions;

public static class RestResponseExtensions
{{
    public static void ThrowIfNotSuccessful(this RestResponse restResponse)
    {{
        if (restResponse.IsSuccessful)
        {{
            return;
        }}

        if (restResponse.Content == null)
        {{
            throw new Exception(""Content is null"");
        }}

        var knownApiException = JsonConvert.DeserializeObject<HttpResponseExceptionBase>(restResponse.Content, new JsonSerializerSettings
        {{
            TypeNameHandling = TypeNameHandling.All,
        }});
        throw knownApiException ?? new InternalServerErrorException(""Unknown API error"");
    }}

    public static T TryDeserialize<T>(this RestResponse restResponse)
    {{
        ThrowIfNotSuccessful(restResponse);
        if (restResponse.Content == null)
        {{
            throw new Exception(""Content is null"");
        }}

        try
        {{
            var response = JsonConvert.DeserializeObject<T>(restResponse.Content, new JsonSerializerSettings
            {{
                TypeNameHandling = TypeNameHandling.All,
            }});
            if (response == null)
            {{
                throw new Exception($""Can not deserialize response as {{typeof(T).Name}}"");
            }}

            return response;
        }}
        catch
        {{
            throw new Exception($""Can not deserialize response as {{typeof(T).Name}}"");
        }}
    }}
}}";

        return new GeneratedFileContent
        {
            FileName = "RestResponseExtensions.cs",
            FolderName = "Extensions",
            FileContent = restResponseExtensionsFileContent,
        };
    }
}