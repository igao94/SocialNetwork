using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Authorization.Constants;

namespace Application.Accounts.Register;

public class RegisterHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<RegisterCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.AccountRepository.IsUsernameTakenAsync(request.Username))
            return Result<AccountDto>.Failure("Username is already taken.");

        if (await unitOfWork.AccountRepository.IsEmailTakenAsync(request.Email))
            return Result<AccountDto>.Failure("Email is already taken.");

        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Username,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth
        };

        var creationResult = await unitOfWork.AccountRepository.CreateUserAsync(user, request.Password);

        if (!creationResult.Succeeded) return Result<AccountDto>.Failure("Unable to register user.");

        var roleResult = await unitOfWork.AccountRepository.AddUserToRoleAsync(user, UserRoles.User);

        if (!roleResult.Succeeded) return Result<AccountDto>.Failure("Failed to add role to user.");

        return Result<AccountDto>.Success(new AccountDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Token = await tokenService.GetTokenAsync(user),
            Image = null
        });
    }
}
