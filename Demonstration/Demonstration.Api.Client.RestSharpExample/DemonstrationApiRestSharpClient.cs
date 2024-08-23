/* Generated file */

using Demonstration.Api.Client.RestSharpExample.Users;
using Demonstration.Api.Client.RestSharpExample.Weather;

namespace Demonstration.Api.Client.RestSharpExample;

public class DemonstrationApiRestSharpClient : IDemonstrationApiClient
{
    public DemonstrationApiRestSharpClient(RestSharp.RestClient client)
    {
        Users = new UsersClient(client);
        Weather = new WeatherClient(client);
    }

    public IUsersClient Users { get; }
    public IWeatherClient Weather { get; }
}
