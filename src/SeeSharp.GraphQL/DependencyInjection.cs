using Microsoft.Extensions.DependencyInjection;

namespace SeeSharp.GraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>();

        return services;
    }
}

