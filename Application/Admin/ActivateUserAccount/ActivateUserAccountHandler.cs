using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.ActivateUserAccount;

public class ActivateUserAccountHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ActivateUserAccountCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(ActivateUserAccountCommand request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetUserByUsernameIncludingInactiveAsync(request.Username);

        if (user is null) return null;

        user.IsActive = true;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Account is active.");
    }
}
