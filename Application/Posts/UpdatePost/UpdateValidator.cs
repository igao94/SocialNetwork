using FluentValidation;

namespace Application.Posts.UpdatePost;

public class UpdateValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdateValidator()
    {
        RuleFor(p => p.Content)
            .NotEmpty()
            .WithMessage("Post must contain text.");
    }
}
