using Newtonsoft.Json;
using Xdd.HttpHelpers.Models.Exceptions;

namespace Xdd.HttpHelpers.Models.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task ThrowIfNotSuccessfulAsync(this HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return;
        }

        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        if (content is null)
        {
            throw new Exception("Content is null");
        }

        var knownApiException = JsonConvert.DeserializeObject<HttpResponseExceptionBase>(
            content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            }
        );

        throw knownApiException ?? new InternalServerErrorException("Unknown API error");
    }

    public static async Task<T> TryDeserializeAsync<T>(this HttpResponseMessage httpResponseMessage)
    {
        await ThrowIfNotSuccessfulAsync(httpResponseMessage);

        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        try
        {
            var response = JsonConvert.DeserializeObject<T>(
                content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                }
            );
            if (response == null)
            {
                throw new Exception($"Can not deserialize response as {typeof(T).Name}");
            }

            return response;
        }
        catch
        {
            throw new Exception($"Can not deserialize response as {typeof(T).Name}");
        }
    }
}