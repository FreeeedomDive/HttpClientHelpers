using RestSharp;
using Xdd.HttpHelpers.Models.Requests;
using BodyParameter = Xdd.HttpHelpers.Models.Requests.Parameters.BodyParameter;
using QueryParameter = Xdd.HttpHelpers.Models.Requests.Parameters.QueryParameter;

namespace Xdd.HttpHelpers.Models.Extensions;

public static class RestClientExtensions
{
    public static async Task<T> MakeRequestAsync<T>(this RestClient restClient, HttpRequest request)
    {
        var response = await restClient.GetResponseAsync(request);
        return response.TryDeserialize<T>();
    }

    public static async Task MakeRequestAsync(this RestClient restClient, HttpRequest request)
    {
        var response = await restClient.GetResponseAsync(request);
        response.ThrowIfNotSuccessful();
    }

    private static async Task<RestResponse> GetResponseAsync(this RestClient restClient, HttpRequest request)
    {
        var restSharpRequest = new RestRequest(request.Url, request.Method.ToRestSharpMethod());
        foreach (var requestParameter in request.Parameters)
        {
            switch (requestParameter)
            {
                case QueryParameter queryParameter:
                    restSharpRequest.AddQueryParameter(queryParameter.Name, queryParameter.Value);
                    break;
                case BodyParameter bodyParameter:
                    restSharpRequest.AddJsonBody(bodyParameter.Body);
                    break;
            }
        }

        return await restClient.ExecuteAsync(restSharpRequest);
    }
}