using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdateEmailCommand;
public class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidator()
    {
        RuleFor(v => v.UserId)
           .NotEmpty();

        RuleFor(v => v.Email)
            .MaximumLength(255)
            .NotEmpty();
    }
   
}
