using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.DeletePost;

public class DeletePostHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotosService photosService) : IRequestHandler<DeletePostCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosAndPostsAndPostPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var post = unitOfWork.PostsRepository.GetPostForUserById(user, request.PostId);

        if (post is null) return null;

        await unitOfWork.ReportsRepository.DeletePostReportsAsync(post.PostId);

        await unitOfWork.LikesRepository.RemovePostLikesAsync(post.PostId);

        await unitOfWork.CommentsRepository.DeleteCommentsByPostIdAsync(post.PostId);

        foreach (var photo in post.PostPhotos) await photosService.DeletePhotoAsync(photo.PublicId);

        unitOfWork.PostsRepository.DeletePost(user, post);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete post.");
    }
}
