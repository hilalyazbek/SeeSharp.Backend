using Microsoft.EntityFrameworkCore;
using SeeSharp.Domain.Models;

namespace SeeSharp.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<BlogPost> BlogPosts { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}