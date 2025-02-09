using Application.Core;
using MediatR;

namespace Application.Admin.ActivateUserAccount;

public record ActivateUserAccountCommand(string Username) : IRequest<Result<Unit>>;
