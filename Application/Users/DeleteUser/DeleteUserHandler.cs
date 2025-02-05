﻿using Application.Core;
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

        var likes = await unitOfWork.LikesRepository.GetLikesByUserIdAsync(user.Id);

        foreach (var like in likes) unitOfWork.LikesRepository.RemoveLike(like);

        foreach (var post in user.Posts)
        {
            var postLikes = await unitOfWork.LikesRepository.GetLikesByPostIdAsync(post.PostId);

            foreach (var like in postLikes) unitOfWork.LikesRepository.RemoveLike(like);

            foreach (var photo in post.PostPhotos) await photosService.DeletePhotoAsync(photo.PublicId);
        }

        unitOfWork.UsersRepository.DeleteUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
