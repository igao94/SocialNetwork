using Application.Helpers;
using FluentValidation;

namespace Application.Accounts.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("First name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("Last name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.Username)
            .NotEmpty()
            .Matches(@"^[a-z0-9]+$")
            .WithMessage("Username must be a single word with only lowercase letters and numbers, without spaces.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .Matches(@"^[^@]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$")
            .WithMessage("Please enter a valid email address.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.");

        RuleFor(u => u.DateOfBirth)
            .NotEmpty()
            .Must(date => date >= DateOnly.Parse("1900-01-01"))
            .WithMessage("Date of birth must be a valid date after January 1st, 1900.")
            .Must(DateExtensions.BeAtLeast16YearsOld)
            .WithMessage("You must be at least 16 years old to register.");
    }
}
