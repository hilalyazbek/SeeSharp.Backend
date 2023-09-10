using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.Authentication.Commands.SignInWithGoogleCommand;

public class SignInWithGoogleCommandValidator : AbstractValidator<SignInWithGoogleCommand>
{
    public SignInWithGoogleCommandValidator()
    {
        RuleFor(v => v.GoogleToken)
            .NotEmpty();
    }
}


