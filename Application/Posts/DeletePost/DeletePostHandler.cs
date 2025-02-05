﻿using Application.Core;
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
            .GetUserWithPhotosAndPostsByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var post = unitOfWork.PostsRepository.GetPostForUserById(user, request.PostId);

        if (post is null) return null;

        var likes = await unitOfWork.LikesRepository.GetLikesByPostIdAsync(post.PostId);

        unitOfWork.LikesRepository.RemoveLikes(likes);

        var comments = await unitOfWork.CommentsRepository.GetCommentsByPostIdAsync(post.PostId);

        unitOfWork.CommentsRepository.RemoveComments(comments);

        foreach (var photo in post.PostPhotos) await photosService.DeletePhotoAsync(photo.PublicId);

        unitOfWork.PostsRepository.DeletePost(user, post);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete post.");
    }
}
