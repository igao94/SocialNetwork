using Application.Core;
using MediatR;

namespace Application.Followers.ToggleFollow;

public record ToggleFollowCommand(string TargetUsername) : IRequest<Result<Unit>>;
