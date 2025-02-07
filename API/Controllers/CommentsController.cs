using Application.Comments.AddComment;
using Application.Comments.DeleteComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CommentsController(IMediator mediator) : BaseApiController
{
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(AddCommentCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        return HandleResult(await mediator.Send(new DeleteCommentCommand(commentId)));
    }
}
