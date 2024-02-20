using FluentValidation;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.FullName)
            .MaximumLength(255)
            .NotEmpty();
    }

}
