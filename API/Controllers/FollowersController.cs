using Application.Followers.GetAllFollows;
using Application.Followers.GetFollowedUsersPost;
using Application.Followers.ToggleFollow;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FollowersController(IMediator mediator) : BaseApiController
{
    [HttpPost("followUser/{username}")]
    public async Task<IActionResult> ToggleFollow(string username)
    {
        return HandleResult(await mediator.Send(new ToggleFollowCommand(username)));
    }

    [HttpGet("{searchTerm}")]
    public async Task<IActionResult> GetUserFollow(string searchTerm)
    {
        return HandleResult(await mediator.Send(new GetAllFollowsQuery(searchTerm)));
    }

    [HttpGet("feed")]
    public async Task<IActionResult> GetFollowedUsersPosts()
    {
        return HandleResult(await mediator.Send(new GetFollowedUsersPostsQuery()));
    }
}
