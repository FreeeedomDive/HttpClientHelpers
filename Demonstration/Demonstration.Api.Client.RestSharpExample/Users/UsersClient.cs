/* Generated file */
using System.Threading.Tasks;

using Xdd.HttpHelpers.Models.Extensions;
using Xdd.HttpHelpers.Models.Requests;

namespace Demonstration.Api.Client.RestSharpExample.Users;

public class UsersClient : IUsersClient
{
    public UsersClient(RestSharp.RestClient client)
    {
        this.client = client;
    }

    public async Task<Demonstration.Api.Dto.UserDto[]> ReadAllAsync()
    {
        var requestBuilder = new RequestBuilder($"api/users/", HttpRequestMethod.GET);
        return await client.MakeRequestAsync<Demonstration.Api.Dto.UserDto[]>(requestBuilder.Build());
    }

    public async Task<Demonstration.Api.Dto.UserDto> ReadAsync(System.Guid id)
    {
        var requestBuilder = new RequestBuilder($"api/users/{id}", HttpRequestMethod.GET);
        return await client.MakeRequestAsync<Demonstration.Api.Dto.UserDto>(requestBuilder.Build());
    }

    public async Task<Demonstration.Api.Dto.UserDto> CreateAsync()
    {
        var requestBuilder = new RequestBuilder($"api/users/", HttpRequestMethod.POST);
        return await client.MakeRequestAsync<Demonstration.Api.Dto.UserDto>(requestBuilder.Build());
    }

    public async Task<Demonstration.Api.Dto.UserDto> UpdateAsync(Demonstration.Api.Dto.UserDto userDto)
    {
        var requestBuilder = new RequestBuilder($"api/users/", HttpRequestMethod.PATCH);
        requestBuilder.WithJsonBody(userDto);
        return await client.MakeRequestAsync<Demonstration.Api.Dto.UserDto>(requestBuilder.Build());
    }

    public async Task<Demonstration.Api.Dto.UserDto[]> SearchAsync(Demonstration.Api.Dto.UsersFilterDto usersFilterDto, int? skip = null, int? take = null, bool includeDeleted = false)
    {
        var requestBuilder = new RequestBuilder($"api/users/search", HttpRequestMethod.POST);
        requestBuilder.WithJsonBody(usersFilterDto);
        requestBuilder.WithQueryParameter("skip", skip);
        requestBuilder.WithQueryParameter("take", take);
        requestBuilder.WithQueryParameter("includeDeleted", includeDeleted);
        return await client.MakeRequestAsync<Demonstration.Api.Dto.UserDto[]>(requestBuilder.Build());
    }

    private readonly RestSharp.RestClient client;
}
