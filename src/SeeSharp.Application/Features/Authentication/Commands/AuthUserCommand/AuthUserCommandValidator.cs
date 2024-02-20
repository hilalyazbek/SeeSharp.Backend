using FluentValidation;

namespace SeeSharp.Application.Features.Authentication.Commands.AuthUserCommand;

public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
{
    public AuthUserCommandValidator()
    {
        RuleFor(v => v.UserName)
            .NotEmpty();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}


