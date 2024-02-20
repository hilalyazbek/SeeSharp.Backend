using FluentValidation;

namespace SeeSharp.Application.Features.Authentication.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.FullName)
            .NotEmpty();

        RuleFor(v => v.Email)
            .NotEmpty();

        RuleFor(v => v.UserName)
            .NotEmpty();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}


