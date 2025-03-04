using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Photos.SetMainPhoto;

public class SetMainPhotoHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<SetMainPhotoCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var photo = user.Photos.FirstOrDefault(p => p.PhotoId == request.PhotoId);

        if (photo is null) return null;

        if (photo.IsMain) return Result<Unit>.Failure("Already main photo.");

        var currentMainPhoto = unitOfWork.PhotosRepository.GetCurrentMainPhoto(user);

        if (currentMainPhoto is not null) currentMainPhoto.IsMain = false;

        photo.IsMain = true;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to change main photo.");
    }
}
