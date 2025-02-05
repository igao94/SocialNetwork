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
            .GetUserWithPhotosAndPostsAndLikesByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        foreach (var photo in user.Photos)
        {
            await photosService.DeletePhotoAsync(photo.PublicId);
        }

        var userLikes = await unitOfWork.LikesRepository.GetLikesByUserIdAsync(user.Id);

        unitOfWork.LikesRepository.RemoveLikes(userLikes);

        var userComments = await unitOfWork.CommentsRepository.GetCommentsByUserIdAsync(user.Id);

        unitOfWork.CommentsRepository.RemoveComments(userComments);

        var postIds = unitOfWork.PostsRepository.GetPostIds(user);

        var postComments = await unitOfWork.CommentsRepository.GetCommentsByPostIdsAsync(postIds);

        unitOfWork.CommentsRepository.RemoveComments(postComments);

        var postLikes = await unitOfWork.LikesRepository.GetLikesByPostIdsAsync(postIds);

        unitOfWork.LikesRepository.RemoveLikes(postLikes);

        var postPhotos = unitOfWork.PostsRepository.GetPostPhotos(user);

        foreach (var photo in postPhotos) await photosService.DeletePhotoAsync(photo.PublicId);

        unitOfWork.UsersRepository.DeleteUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
