using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;
using SeeSharp.Infrastructure.Identity;

namespace SeeSharp.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext()
    {
            
    }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SeeSharpBlog;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True");
        // optionsBuilder.UseSqlServer("Server =.\\SQLExpress; Database = SeeSharpBlog; Trusted_Connection = True; TrustServerCertificate = True");
    }
}

