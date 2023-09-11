using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SeeSharp.Domain.Models;

public class BlogPost : Entity
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    public string AuthorId { get; set; } = string.Empty;

    public ApplicationUser Author { get; set; } = null!;

    public string Content { get; set; } = string.Empty;
}