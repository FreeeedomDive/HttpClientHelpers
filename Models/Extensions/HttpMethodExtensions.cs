using RestSharp;
using Xdd.HttpHelpers.Models.Requests;

namespace Xdd.HttpHelpers.Models.Extensions;

public static class HttpMethodExtensions
{
    public static Method ToRestSharpMethod(this HttpRequestMethod httpRequestMethod)
    {
        return httpRequestMethod switch
        {
            HttpRequestMethod.GET => Method.Get,
            HttpRequestMethod.POST => Method.Post,
            HttpRequestMethod.PUT => Method.Put,
            HttpRequestMethod.DELETE => Method.Delete,
            HttpRequestMethod.PATCH => Method.Patch,
            _ => throw new ArgumentOutOfRangeException(nameof(httpRequestMethod), httpRequestMethod, null),
        };
    }
}