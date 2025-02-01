using Application.Accounts.GetCurrentUser;
using Application.Accounts.Login;
using Application.Accounts.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController(IMediator mediator) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpGet("currentUser")]
    public async Task<IActionResult> GetCurrentUser()
    {
        return HandleResult(await mediator.Send(new GetCurrentUserQuery()));
    }
}
