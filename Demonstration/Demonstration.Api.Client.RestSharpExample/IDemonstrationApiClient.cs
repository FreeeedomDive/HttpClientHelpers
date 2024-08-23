/* Generated file */

using Demonstration.Api.Client.RestSharpExample.Users;
using Demonstration.Api.Client.RestSharpExample.Weather;

namespace Demonstration.Api.Client.RestSharpExample;

public interface IDemonstrationApiClient
{
    IUsersClient Users { get; }
    IWeatherClient Weather { get; }
}
