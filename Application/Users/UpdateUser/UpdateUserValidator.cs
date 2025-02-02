using Application.Helpers;
using FluentValidation;

namespace Application.Users.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty();

        RuleFor(u => u.LastName).NotEmpty();

        RuleFor(u => u.DateOfBirth)
            .NotEmpty()
            .Must(date => date >= DateOnly.Parse("1900-01-01"))
            .WithMessage("Date of birth must be a valid date after January 1st, 1900.")
            .Must(DateExtensions.BeAtLeast16YearsOld)
            .WithMessage("You must be at least 16 years old.");
    }
}
