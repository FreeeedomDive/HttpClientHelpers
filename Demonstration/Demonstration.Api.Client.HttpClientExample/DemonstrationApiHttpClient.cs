/* Generated file */

using Demonstration.Api.Client.HttpClientExample.Users;

namespace Demonstration.Api.Client.HttpClientExample;

public class DemonstrationApiHttpClient : IDemonstrationApiClient
{
    public DemonstrationApiHttpClient(System.Net.Http.HttpClient client)
    {
        Users = new UsersClient(client);
    }

    public IUsersClient Users { get; }
}
