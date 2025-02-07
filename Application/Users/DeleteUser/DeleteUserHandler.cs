using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.DeleteUser;

public class DeleteUserHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotosService photosService) : IRequestHandler<DeleteUserCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosAndPostsAndPostPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        foreach (var photo in user.Photos)
        {
            await photosService.DeletePhotoAsync(photo.PublicId);
        }

        await unitOfWork.FollowingsRepository.RemoveAllFollowsForUserAsync(user.Id);

        await unitOfWork.LikesRepository.RemoveUserLikesAsync(user.Id);

        await unitOfWork.CommentsRepository.DeleteCommentsByUserIdAsync(user.Id);

        var postIds = unitOfWork.PostsRepository.GetPostIds(user);

        await unitOfWork.ReportsRepository.DeleteAllPostsReportsAsync(postIds);

        await unitOfWork.ReportsRepository.DeleteAllUserReportsAsync(user.Id);

        await unitOfWork.CommentsRepository.DeleteCommentsByPostIdsAsync(postIds);

        await unitOfWork.LikesRepository.RemovePostsLikesAsync(postIds);

        var postPhotos = unitOfWork.PostsRepository.GetPostPhotos(user);

        foreach (var photo in postPhotos) await photosService.DeletePhotoAsync(photo.PublicId);

        unitOfWork.UsersRepository.DeleteUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
