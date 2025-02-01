using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.GetCurrentUser;

public class GetCurrentUserHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    ITokenService tokenService) : IRequestHandler<GetCurrentUserQuery, Result<AccountDto>?>
{
    public async Task<Result<AccountDto>?> Handle(GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository
            .GetUserByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null || user.UserName is null || user.Email is null) return null;

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
