using FluentValidation;

namespace Application.Accounts.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .Matches(@"^[^@]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$")
            .WithMessage("Please enter a valid email address.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
