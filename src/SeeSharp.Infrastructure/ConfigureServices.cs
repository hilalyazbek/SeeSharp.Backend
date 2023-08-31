using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SeeSharp.Infrastructure.DbContexts;
using SeeSharp.Application.Common.Interfaces;
using Ardalis.GuardClauses;
using SeeSharp.Infrastructure.Identity;

namespace SeeSharp.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        var conn = configuration.GetConnectionString("DefaultConnection");

        // Add MSSQL DB Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(conn));

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        return services;

    }
}

