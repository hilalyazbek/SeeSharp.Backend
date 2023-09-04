using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

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


