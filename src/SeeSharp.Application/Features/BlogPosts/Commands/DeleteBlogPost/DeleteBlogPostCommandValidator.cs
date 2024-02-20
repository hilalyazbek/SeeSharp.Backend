using FluentValidation;

namespace SeeSharp.Application.Features.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
{
    public DeleteBlogPostCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}


