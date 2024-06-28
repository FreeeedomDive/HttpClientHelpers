/* Generated file */

using Demonstration.Api.Client.RestSharpExample.Users;

namespace Demonstration.Api.Client.RestSharpExample;

public class DemonstrationApiRestSharpClient : IDemonstrationApiClient
{
    public DemonstrationApiRestSharpClient(RestSharp.RestClient client)
    {
        Users = new UsersClient(client);
    }

    public IUsersClient Users { get; }
}
