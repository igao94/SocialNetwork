using FluentValidation;

namespace Application.Followers.ToggleFollow;

public class FollowValidator : AbstractValidator<ToggleFollowCommand>
{
    public FollowValidator()
    {
        RuleFor(f => f.TargetUsername)
        .NotEmpty()
        .WithMessage("Please enter the username of the user you wish to follow.");
    }
}
