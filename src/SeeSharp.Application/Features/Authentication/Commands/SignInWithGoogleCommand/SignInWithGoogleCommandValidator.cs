using FluentValidation;

namespace SeeSharp.Application.Features.Authentication.Commands.SignInWithGoogleCommand;

public class SignInWithGoogleCommandValidator : AbstractValidator<SignInWithGoogleCommand>
{
    public SignInWithGoogleCommandValidator()
    {
        RuleFor(v => v.GoogleToken)
            .NotEmpty();
    }
}


