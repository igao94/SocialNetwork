using Application.Core;
using MediatR;

namespace Application.Likes.GetLikeStatus;

public record GetLikeStatusQuery(int PostId) : IRequest<Result<bool>>;
