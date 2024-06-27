/* Generated file */
using System.Threading.Tasks;

namespace Demonstration.Api.Client.Users;

public interface IUsersClient
{
    Task<Demonstration.Api.Dto.UserDto[]> ReadAllAsync();
    Task<Demonstration.Api.Dto.UserDto> ReadAsync(System.Guid id);
    Task<Demonstration.Api.Dto.UserDto> CreateAsync();
    Task<Demonstration.Api.Dto.UserDto> UpdateAsync(Demonstration.Api.Dto.UserDto userDto);
    Task<Demonstration.Api.Dto.UserDto[]> SearchAsync(Demonstration.Api.Dto.UsersFilterDto usersFilterDto, int? skip = null, int? take = null, bool includeDeleted = false);
}
