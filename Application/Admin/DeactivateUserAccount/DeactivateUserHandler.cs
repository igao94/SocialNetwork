using Application.Core;
using Domain.Interfaces;
using MediatR;
using Persistence.Authorization.Constants;

namespace Application.Admin.DeactivateUserAccount;

public class DeactivateUserHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeactivateUserAccountCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeactivateUserAccountCommand request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetUserByUsernameIncludingInactiveAsync(request.Username);

        if (user is null) return null;

        var IsUserAdmin = await unitOfWork.AccountRepository.IsUserInRoleAsync(user, UserRoles.Admin);

        if (IsUserAdmin) return Result<Unit>.Failure("Can't deactive admin account.");

        user.IsActive = false;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("User account is deactivated already.");
    }
}
