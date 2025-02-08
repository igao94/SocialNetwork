using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.ResetPassword;

public class ResetPasswordHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordDto>>
{
    public async Task<Result<ResetPasswordDto>> Handle(ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByEmailAsync(request.Email);

        if (user is null) return Result<ResetPasswordDto>.Failure("Invalid email address.");

        var IsOldPassword = await unitOfWork.AccountRepository.CheckPasswordAsync(user, request.NewPassword);

        if (IsOldPassword) return Result<ResetPasswordDto>
                .Failure("New password cannot be the same as old password.");

        var resetToken = await unitOfWork.AccountRepository.GenerateResetPasswordTokenAsync(user);

        var result = await unitOfWork.AccountRepository
            .ResetPasswordAsync(user, resetToken, request.NewPassword);

        if (!result.Succeeded) return Result<ResetPasswordDto>.Failure("Failed to reset password.");

        return Result<ResetPasswordDto>.Success(new ResetPasswordDto
        {
            Email = request.Email,
            Password = request.NewPassword,
            Token = await tokenService.GetTokenAsync(user)
        });
    }
}
