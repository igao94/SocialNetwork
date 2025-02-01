using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<AccountDto>>;
