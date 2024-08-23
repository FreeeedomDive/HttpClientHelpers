/* Generated file */

using Demonstration.Api.Client.HttpClientExample.Users;
using Demonstration.Api.Client.HttpClientExample.Weather;

namespace Demonstration.Api.Client.HttpClientExample;

public interface IDemonstrationApiClient
{
    IUsersClient Users { get; }
    IWeatherClient Weather { get; }
}
