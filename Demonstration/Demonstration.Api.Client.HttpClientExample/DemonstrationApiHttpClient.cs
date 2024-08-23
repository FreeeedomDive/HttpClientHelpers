/* Generated file */

using Demonstration.Api.Client.HttpClientExample.Users;
using Demonstration.Api.Client.HttpClientExample.Weather;

namespace Demonstration.Api.Client.HttpClientExample;

public class DemonstrationApiHttpClient : IDemonstrationApiClient
{
    public DemonstrationApiHttpClient(System.Net.Http.HttpClient client)
    {
        Users = new UsersClient(client);
        Weather = new WeatherClient(client);
    }

    public IUsersClient Users { get; }
    public IWeatherClient Weather { get; }
}
