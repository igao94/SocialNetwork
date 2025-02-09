using Application.Core;
using MediatR;

namespace Application.Admin.DeactivateUserAccount;

public record DeactivateUserAccountCommand(string Username) : IRequest<Result<Unit>>;
