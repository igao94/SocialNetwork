using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Login;

public class LoginHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<AccountDto>?>
{
    public async Task<Result<AccountDto>?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserWithPhotosByEmailAsync(request.Email);

        if (user is null || user.UserName is null || user.Email is null) return null;

        var result = await unitOfWork.AccountRepository.CheckPasswordAsync(user, request.Password);

        if (!result) return Result<AccountDto>.Failure("Invalid username or password.");

        return Result<AccountDto>.Success(new AccountDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Token = await tokenService.GetTokenAsync(user),
            PhotoUrl = unitOfWork.UsersRepository.GetMainPhoto(user)
        });
    }
}
