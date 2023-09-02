using System.Text;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Infrastructure.Identity;

namespace SeeSharp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        AddAuthenticationAndAuthorization(services, configuration);
        services.AddAuthentication();

        services.AddHttpContextAccessor();

        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<SignInManager<ApplicationUser>>();

        return services;
    }

    private static void AddAuthenticationAndAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        // Add Jwt Token Options
        var jwtSettings = Guard.Against.Null(configuration.GetSection("JwtOptions"));

        var secret = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Secret"));
        var issuer = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Issuer"));
        var audience = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Audience"));

        var key = Encoding.ASCII.GetBytes(secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Default", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

            options.AddPolicy("Administrator", new AuthorizationPolicyBuilder()
                .RequireRole("Administrator")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        });
    }
}

