/* Generated file */

using Demonstration.Api.Client.Users;

namespace Demonstration.Api.Client;

public class DemonstrationApiClient : IDemonstrationApiClient
{
    public DemonstrationApiClient(RestSharp.RestClient restClient)
    {
        Users = new UsersClient(restClient);
    }

    public IUsersClient Users { get; }
}
