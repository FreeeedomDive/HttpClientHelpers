/* Generated file */

using Demonstration.Api.Client.RestSharpExample.Users;

namespace Demonstration.Api.Client.RestSharpExample;

public class DemonstrationApiRestSharpClient : IDemonstrationApiClient
{
    public DemonstrationApiRestSharpClient(RestSharp.RestClient restClient)
    {
        Users = new UsersClient(restClient);
    }

    public IUsersClient Users { get; }
}
