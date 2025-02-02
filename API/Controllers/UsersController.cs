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
    public async Task<IActionResult> GetAllUsers(string? searchTerm)
    {
        return HandleResult(await mediator.Send(new GetAllUsersQuery(searchTerm)));
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
}
