using FluentValidation;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdatePasswordCommand;
public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(v => v.UserId)
           .NotEmpty();

        RuleFor(v => v.OldPassword)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(v => v.NewPassword)
            .MaximumLength(255)
            .NotEmpty();
    }

}
