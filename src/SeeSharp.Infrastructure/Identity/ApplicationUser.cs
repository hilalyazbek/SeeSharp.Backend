using System;
using Microsoft.AspNetCore.Identity;

namespace SeeSharp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}

