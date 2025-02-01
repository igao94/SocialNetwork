using FluentValidation;

namespace Application.Accounts.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand> 
{
    public RegisterValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty();

        RuleFor(u => u.LastName).NotEmpty();

        RuleFor(u => u.Username).NotEmpty();

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .Matches(@"^[^@]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$")
            .WithMessage("Please enter a valid email address.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(20)
            .Matches(@"[\W_]")
            .WithMessage("Password must contain at least one non-alphanumeric character.");
    }
}
