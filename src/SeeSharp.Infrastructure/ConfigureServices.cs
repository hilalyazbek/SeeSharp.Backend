using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Infrastructure.DbContexts;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        var conn = configuration.GetConnectionString("DefaultConnection");

        // Add MSSQL DB Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(conn));
        
        return services;

    }
}

