using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.BlogPosts.Commands.CreateBlogPost;

public class DeleteBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public DeleteBlogPostCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(v => v.Category)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(v => v.Author)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(v => v.Content)
            .NotEmpty();
    }
}


