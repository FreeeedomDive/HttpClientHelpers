using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.Extensions;

public static class ClientTypeExtensions
{
    public static string ToClientTypeFullName(this ClientType clientType)
    {
        return clientType switch
        {
            ClientType.RestSharp => "RestSharp.RestClient",
            ClientType.SystemNetHttpClient => "System.Net.Http.HttpClient",
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}