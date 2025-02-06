using FluentValidation;

namespace Application.Followers.GetAllFollows;

public class GetAllFollowValidator : AbstractValidator<GetAllFollowsQuery>
{
    public GetAllFollowValidator()
    {
        RuleFor(f => f.SearchTerm)
            .NotEmpty()
            .WithMessage("Type in followers or following.");
    }
}
