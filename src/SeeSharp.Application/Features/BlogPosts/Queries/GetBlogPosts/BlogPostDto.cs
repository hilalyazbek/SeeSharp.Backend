using System;
using System.ComponentModel.DataAnnotations;

namespace SeeSharp.Application.Features.BlogPosts.Queries.GetBlogPosts;

public class BlogPostDto
{
    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string DateCreated { get; set; } = DateTime.Today.ToShortDateString();
}