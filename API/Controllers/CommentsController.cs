using Application.Comments.AddComment;
using Application.Comments.DeleteComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CommentsController(IMediator mediator) : BaseApiController
{
    [HttpPost("{postId}")]
    public async Task<IActionResult> AddComment(int postId, string content)
    {
        return HandleResult(await mediator.Send(new AddCommentCommand(postId, content)));
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        return HandleResult(await mediator.Send(new DeleteCommentCommand(commentId)));
    }
}
