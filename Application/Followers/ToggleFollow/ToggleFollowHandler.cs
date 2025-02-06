using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Followers.ToggleFollow;

public class ToggleFollowHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<ToggleFollowCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(ToggleFollowCommand request, CancellationToken cancellationToken)
    {
        var observer = await unitOfWork.UsersRepository.GetUserByIdAsync(userAccessor.GetCurrentUserId());

        var target = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.TargetUsername);

        if (observer is null || target is null) return null;

        if (observer.UserName == target.UserName) return Result<Unit>.Failure("You can't follow yourself.");

        var following = await unitOfWork.FollowingsRepository.GetFollowingAsync(observer.Id, target.Id);

        if (following is null)
        {
            following = new AppUserFollowing
            {
                Observer = observer,
                ObserverId = observer.Id,
                Target = target,
                TargetId = target.Id
            };

            unitOfWork.FollowingsRepository.AddFollowing(following);
        }
        else
        {
            unitOfWork.FollowingsRepository.RemoveFollowing(following);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update following.");
    }
}
