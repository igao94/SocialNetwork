using Application.GDPR.GetUserData;
using Application.Users;
using Application.Users.DeleteUser;
using Application.Users.GetAllUsers;
using Application.Users.GetUserByUsername;
using Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] UsersParams usersParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllUsersQuery(usersParams)));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        return HandleResult(await mediator.Send(new GetUserByUsernameQuery(username)));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        return HandleResult(await mediator.Send(new DeleteUserCommand()));
    }

    [HttpGet("download-user-data")]
    public async Task<IActionResult> DownloadUserData()
    {
        return HandleResult(await mediator.Send(new GetUserDataQuery()));
    }
}
