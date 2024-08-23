/* Generated file */
using System.Threading.Tasks;

using Xdd.HttpHelpers.Models.Extensions;
using Xdd.HttpHelpers.Models.Requests;

namespace Demonstration.Api.Client.RestSharpExample.Weather;

public class WeatherClient : IWeatherClient
{
    public WeatherClient(RestSharp.RestClient client)
    {
        this.client = client;
    }

    public async Task<int> GetTemperatureAsync()
    {
        var requestBuilder = new RequestBuilder($"/api/weather/temperature", HttpRequestMethod.GET);
        return await client.MakeRequestAsync<int>(requestBuilder.Build());
    }

    private readonly RestSharp.RestClient client;
}
