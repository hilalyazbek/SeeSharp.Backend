using System;
using System.ComponentModel.DataAnnotations;

namespace SeeSharp.Application.Features.BlogPosts.Queries;

public class BlogPostDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string DateCreated { get; set; } = DateTime.Today.ToShortDateString();
}