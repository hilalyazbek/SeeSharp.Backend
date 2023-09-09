using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.Authentication.Commands.GoogleRegisterUserCommand;

public class GoogleRegisterUserCommandValidator : AbstractValidator<GoogleRegisterUserCommand>
{
    public GoogleRegisterUserCommandValidator()
    {
        RuleFor(v => v.GoogleToken)
            .NotEmpty();
    }
}


