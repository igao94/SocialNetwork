using Application.Posts;
using Application.Posts.AddPost;
using Application.Posts.DeletePost;
using Application.Posts.GetAllPosts;
using Application.Posts.GetPostById;
using Application.Posts.UpdatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllPosts([FromQuery] PostsParams postParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllPostsQuery(postParams)));
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostById(int postId)
    {
        return HandleResult(await mediator.Send(new GetPostByIdQuery(postId)));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] AddPostCommand command)
    {
        var result = await mediator.Send(command);

        return HandleResult(result, nameof(GetPostById), new { postId = result.Value?.PostId });
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(int postId, UpdatePostCommand command)
    {
        command.PostId = postId;

        return HandleResult(await mediator.Send(command));
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        return HandleResult(await mediator.Send(new DeletePostCommand(postId)));
    }
}
