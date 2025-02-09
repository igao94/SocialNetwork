using Application.Likes;
using Application.Likes.GetLikeStatus;
using Application.Likes.GetPostLikes;
using Application.Likes.ToggleLike;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LikesController(IMediator mediator) : BaseApiController
{
    [HttpGet("users-who-liked-post")]
    public async Task<IActionResult> GetUsersWhoLikedPost([FromQuery] LikesParams likesParams)
    {
        return HandlePagedResult(await mediator.Send(new GetPostLikesQuery(likesParams)));
    }

    [HttpGet("status/{postId}")]
    public async Task<IActionResult> GetLikeStatus(int postId)
    {
        return HandleResult(await mediator.Send(new GetLikeStatusQuery(postId)));
    }

    [HttpPost("{postId}")]
    public async Task<IActionResult> ToggleLike(int postId)
    {
        return HandleResult(await mediator.Send(new ToggleLikeCommand(postId)));
    }
}
