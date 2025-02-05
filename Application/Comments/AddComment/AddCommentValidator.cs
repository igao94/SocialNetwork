using FluentValidation;

namespace Application.Comments.AddComment;

public class AddCommentValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty()
            .WithMessage("Content can't be empty.");
    }
}
