using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Likes.ToggleLike;

public class ToggleLikeHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<ToggleLikeCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var post = await unitOfWork.PostsRepository.GetPostByIdAsync(request.PostId);

        if (post is null) return null;

        var existingLike = await unitOfWork.LikesRepository.GetLikeByIdAsync(userId, post.PostId);

        if (existingLike is null)
        {
            var like = new AppUserPostLike
            {
                AppUserId = userId,
                PostId = post.PostId
            };

            unitOfWork.LikesRepository.AddLike(like);
        }
        else
        {
            unitOfWork.LikesRepository.RemoveLike(existingLike);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Problem adding like.");
    }
}
