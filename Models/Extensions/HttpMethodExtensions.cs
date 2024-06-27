using RestSharp;
using HttpMethod = Xdd.HttpHelpers.Models.Requests.HttpMethod;

namespace Xdd.HttpHelpers.Models.Extensions;

public static class HttpMethodExtensions
{
    public static Method ToRestSharpMethod(this HttpMethod httpMethod)
    {
        return httpMethod switch
        {
            HttpMethod.GET => Method.Get,
            HttpMethod.POST => Method.Post,
            HttpMethod.PUT => Method.Put,
            HttpMethod.DELETE => Method.Delete,
            HttpMethod.PATCH => Method.Patch,
            _ => throw new ArgumentOutOfRangeException(nameof(httpMethod), httpMethod, null),
        };
    }
}