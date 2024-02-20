using Microsoft.AspNetCore.Identity;

namespace SeeSharp.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}

