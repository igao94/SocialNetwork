using FluentValidation;

namespace Application.Posts.AddPost;

public class PostValidator : AbstractValidator<AddPostCommand>
{
    public PostValidator()
    {
        RuleFor(p => p.Content)
            .NotEmpty()
            .WithMessage("Post must contain text.");
    }
}
