using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.DeletePostAsAdmin;

public class DeletePostAsAdminHandler(IUnitOfWork unitOfWork,
    IPhotosService photosService) : IRequestHandler<DeletePostAsAdminCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeletePostAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var post = await unitOfWork.PostsRepository.GetPostWithPostPhotosByIdAsync(request.PostId);

        if (post is null) return null;

        await unitOfWork.ReportsRepository.DeletePostReportsAsync(post.PostId);

        await unitOfWork.LikesRepository.RemovePostLikesAsync(post.PostId);

        await unitOfWork.CommentsRepository.DeleteCommentsByPostIdAsync(post.PostId);

        foreach (var photo in post.PostPhotos) await photosService.DeletePhotoAsync(photo.PublicId);

        unitOfWork.PostsRepository.DeletePost(post);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete post.");
    }
}
