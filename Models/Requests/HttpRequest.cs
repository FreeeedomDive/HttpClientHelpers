using Xdd.HttpHelpers.Models.Requests.Parameters;

namespace Xdd.HttpHelpers.Models.Requests;

public class HttpRequest
{
    public string Url { get; set; }
    public HttpRequestMethod Method { get; set; }
    public List<IRequestParameter> Parameters { get; set; }
}