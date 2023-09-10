using System;
using System.ComponentModel.DataAnnotations;

namespace SeeSharp.Application.Features.BlogPosts.Queries;

public class BlogPostDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Category { get; set; }

    public string? FullName { get; set; }

    public string? Content { get; set; }

    public string? DateCreated { get; set; }
}