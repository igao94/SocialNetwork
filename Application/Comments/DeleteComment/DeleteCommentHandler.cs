using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.DeleteComment;

public class DeleteCommentHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<DeleteCommentCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var comment = await unitOfWork.CommentsRepository.GetCommentByUserIdAsync(userId, request.CommentId);

        if (comment is null) return null;

        unitOfWork.CommentsRepository.RemoveComment(comment);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to remove comment.");
    }
}
