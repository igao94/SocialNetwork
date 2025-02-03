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
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        foreach (var photo in user.Photos)
        {
            await photosService.DeletePhotoAsync(photo.PublicId);
        }

        unitOfWork.UsersRepository.DeleteUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
