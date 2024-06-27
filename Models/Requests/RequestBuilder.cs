using Xdd.HttpHelpers.Models.Requests.Parameters;

namespace Xdd.HttpHelpers.Models.Requests;

public class RequestBuilder
{
    public RequestBuilder(string url, HttpRequestMethod httpRequestMethod)
    {
        request = new HttpRequest
        {
            Url = url,
            Method = httpRequestMethod,
            Parameters = new List<IRequestParameter>(),
        };
    }

    public RequestBuilder WithQueryParameter<T>(string name, T? value)
    {
        request.Parameters.Add(new QueryParameter
        {
            Name = name,
            Value = value?.ToString(),
        });
        return this;
    }

    public RequestBuilder WithJsonBody<T>(T body) where T : notnull
    {
        if (request.Parameters.FirstOrDefault(x => x is BodyParameter) is not null)
        {
            throw new ArgumentException("Can't add second body to HttpRequest");
        }

        request.Parameters.Add(new BodyParameter
        {
            Body = body,
        });
        return this;
    }

    public HttpRequest Build()
    {
        return request;
    }

    private readonly HttpRequest request;
}