using FluentValidation;

namespace SeeSharp.Application.Features.BlogPosts.Commands.UpdateBlogPost;

public class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostCommand>
{
    public UpdateBlogPostCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Title)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(v => v.Category)
            .MaximumLength(255)
            .NotEmpty();
        ;
        RuleFor(v => v.Content)
            .NotEmpty();
    }
}


