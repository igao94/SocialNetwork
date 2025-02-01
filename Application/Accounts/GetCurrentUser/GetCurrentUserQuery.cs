using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<Result<AccountDto>>;

