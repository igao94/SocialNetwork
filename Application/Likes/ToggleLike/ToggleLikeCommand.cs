using Application.Core;
using MediatR;

namespace Application.Likes.ToggleLike;

public record ToggleLikeCommand(int PostId) : IRequest<Result<Unit>>;
