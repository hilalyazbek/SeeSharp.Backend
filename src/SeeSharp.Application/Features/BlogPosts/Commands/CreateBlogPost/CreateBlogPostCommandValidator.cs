using FluentValidation;

namespace SeeSharp.Application.Features.BlogPosts.Commands.CreateBlogPost;

public class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(v => v.Category)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(v => v.Content)
            .NotEmpty();
    }
}


