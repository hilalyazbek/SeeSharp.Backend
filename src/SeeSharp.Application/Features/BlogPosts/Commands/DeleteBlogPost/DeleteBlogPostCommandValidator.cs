using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
{
    public DeleteBlogPostCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}


