using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Likes.GetLikeStatus;

public class GetLikeStatusHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<GetLikeStatusQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(GetLikeStatusQuery request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var existingLike = await unitOfWork.LikesRepository.GetLikeByIdAsync(userId, request.PostId);

        if (existingLike is null) return Result<bool>.Failure("You didn't like this post.");

        return Result<bool>.Success(true);
    }
}
