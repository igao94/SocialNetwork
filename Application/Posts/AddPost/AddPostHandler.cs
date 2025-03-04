using Application.Core;
using Application.Interfaces;
using Application.Posts.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.AddPost;

public class AddPostHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotosService photosService,
    IMapper mapper) : IRequestHandler<AddPostCommand, Result<PostDto>?>
{
    public async Task<Result<PostDto>?> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var post = new Post
        {
            Content = request.Content,
        };

        if (request.Files is not null)
        {
            const int MaxPhotos = 5;

            if (request.Files.Count > MaxPhotos)
                return Result<PostDto>.Failure("You can only add up to 5 photos per post.");

            foreach (var file in request.Files)
            {
                var photoUploadResult = await photosService.AddPhotoAsync(file);

                if (photoUploadResult is null)
                    return Result<PostDto>.Failure("Failed to upload photo to Clouinary.");

                var photo = new PostPhoto
                {
                    PublicId = photoUploadResult.PublicId,
                    Url = photoUploadResult.Url
                };

                post.PostPhotos.Add(photo);
            }
        }

        user.Posts.Add(post);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<PostDto>.Success(mapper.Map<PostDto>(post))
            : Result<PostDto>.Failure("Failed to create a post.");
    }
}
