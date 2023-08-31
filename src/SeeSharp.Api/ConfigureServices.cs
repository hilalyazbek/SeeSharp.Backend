using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SeeSharp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
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
       

        return services;
    }
}

