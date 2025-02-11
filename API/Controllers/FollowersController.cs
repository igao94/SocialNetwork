using Application.Followers;
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

    [HttpGet]
    public async Task<IActionResult> GetUserFollow([FromQuery] FollowersParams followersParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllFollowsQuery(followersParams)));
    }

    [HttpGet("feed")]
    public async Task<IActionResult> GetFollowedUsersPosts([FromQuery] FeedParams feedParams)
    {
        return HandlePagedResult(await mediator.Send(new GetFollowedUsersPostsQuery(feedParams)));
    }
}
