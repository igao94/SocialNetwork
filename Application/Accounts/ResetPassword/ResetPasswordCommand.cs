using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.ResetPassword;

public record ResetPasswordCommand(string Email, string NewPassword) : IRequest<Result<ResetPasswordDto>>;
