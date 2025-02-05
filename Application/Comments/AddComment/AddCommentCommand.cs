using Application.Comments.DTOs;
using Application.Core;
using MediatR;

namespace Application.Comments.AddComment;

public record AddCommentCommand(int PostId, string Content) : IRequest<Result<CommentDto>>; 
