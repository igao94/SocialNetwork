using Application.Core;
using Application.Interfaces;
using Application.Photos.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Photos.AddPhoto;

public class AddPhotoHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotosService photosService,
    IMapper mapper) : IRequestHandler<AddPhotoCommand, Result<PhotoDto>?>
{
    public async Task<Result<PhotoDto>?> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var photoUploadResult = await photosService.AddPhotoAsync(request.File);

        if (photoUploadResult is null) return Result<PhotoDto>.Failure("Failed to add photo to cloudinary.");

        var photo = new Photo
        {
            Url = photoUploadResult.Url,
            PublicId = photoUploadResult.PublicId
        };

        if (user.Photos.Count == 0) photo.IsMain = true;

        user.Photos.Add(photo);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<PhotoDto>.Success(mapper.Map<PhotoDto>(photo))
            : Result<PhotoDto>.Failure("Failed to add photo.");
    }
}
