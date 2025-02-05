using Application.Core;
using MediatR;

namespace Application.Comments.DeleteComment;

public record DeleteCommentCommand(int CommentId) : IRequest<Result<Unit>>;
