using Newtonsoft.Json;
using Xdd.HttpHelpers.Models.Requests;
using Xdd.HttpHelpers.Models.Requests.Parameters;

namespace Xdd.HttpHelpers.Models.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> MakeRequestAsync<T>(this HttpClient httpClient, HttpRequest request)
    {
        var response = await httpClient.GetResponseAsync(request);
        return await response.TryDeserializeAsync<T>();
    }

    public static async Task MakeRequestAsync(this HttpClient httpClient, HttpRequest request)
    {
        var response = await httpClient.GetResponseAsync(request);
        await response.ThrowIfNotSuccessfulAsync();
    }

    private static async Task<HttpResponseMessage> GetResponseAsync(this HttpClient httpClient, HttpRequest request)
    {
        var url = request.Url;
        var queryParameters = request
                              .Parameters
                              .OfType<QueryParameter>()
                              .Where(x => x.Value is not null)
                              .Select(x => $"{x.Name}={x.Value}")
                              .ToArray();

        if (queryParameters.Length > 0)
        {
            url += $"?{string.Join("&", queryParameters)}";
        }

        var bodyParameter = request.Parameters.OfType<BodyParameter>().FirstOrDefault();
        var body = bodyParameter is null ? null : new StringContent(JsonConvert.SerializeObject(
            bodyParameter.Body, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            }
        ));
        return request.Method switch
        {
            HttpRequestMethod.GET => await httpClient.GetAsync(url),
            HttpRequestMethod.POST => await httpClient.PostAsync(url, body),
            HttpRequestMethod.PUT => await httpClient.PutAsync(url, body),
            HttpRequestMethod.DELETE => await httpClient.DeleteAsync(url),
            HttpRequestMethod.PATCH => await httpClient.PatchAsync(url, body),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}