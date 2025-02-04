using Application.Core;
using MediatR;

namespace Application.Posts.DeletePost;

public record DeletePostCommand(int PostId) : IRequest<Result<Unit>>;
