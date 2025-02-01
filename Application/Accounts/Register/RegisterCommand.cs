using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Register;

public record RegisterCommand(string FirstName,
    string LastName,
    string Username,
    string Email,
    DateOnly DateOfBirth,
    string Password) : IRequest<Result<AccountDto>>;
