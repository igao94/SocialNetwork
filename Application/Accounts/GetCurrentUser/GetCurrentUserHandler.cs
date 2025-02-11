using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Application.Photos.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.GetCurrentUser;

public class GetCurrentUserHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<GetCurrentUserQuery, Result<CurrentUserDto>?>
{
    public async Task<Result<CurrentUserDto>?> Handle(GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null || user.UserName is null || user.Email is null) return null;

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            MainPhotoUrl = unitOfWork.UsersRepository.GetMainPhoto(user),
            Photos = user.Photos.Select(p => new PhotoDto
            {
                PhotoId = p.PhotoId,
                Url = p.Url,
                IsMain = p.IsMain
            }).ToList()
        });
    }
}
