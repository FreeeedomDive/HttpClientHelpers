using Demonstration.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Demonstration.Api.Controllers;

/// <summary>
///     Basic controller with some DTOs and different HTTP methods and different parameters in route, query and body
/// </summary>
[Route("api/users")]
public class UsersController : Controller
{
    [HttpGet]
    public ActionResult<UserDto[]> ReadAll()
    {
        return Array.Empty<UserDto>();
    }

    [HttpGet("{id:guid}")]
    public ActionResult<UserDto> Read([FromRoute] Guid id)
    {
        return new UserDto
        {
            Id = Guid.NewGuid(),
        };
    }

    [HttpPost]
    public ActionResult<UserDto> Create()
    {
        return new UserDto
        {
            Id = Guid.NewGuid(),
        };
    }

    [HttpPatch]
    public ActionResult<UserDto> Update([FromBody] UserDto userDto)
    {
        return userDto;
    }

    /// <summary>
    ///     This method has optional parameters, client's method will also be with this optional parameters
    /// </summary>
    [HttpPost("search")]
    public ActionResult<UserDto[]> Search(
        [FromBody] UsersFilterDto usersFilterDto,
        [FromQuery] int? skip = null,
        [FromQuery] int? take = null,
        [FromQuery] bool includeDeleted = false
    )
    {
        return Array.Empty<UserDto>();
    }
}