using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.Authentication.Commands.AuthUserCommand;

public class GoogleAuthUserCommandValidator : AbstractValidator<GoogleAuthUserCommand>
{
    public GoogleAuthUserCommandValidator()
    {
        RuleFor(v => v.GoogleToken)
            .NotEmpty();
    }
}


