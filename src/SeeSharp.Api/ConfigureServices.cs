using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Api.GraphQL;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Infrastructure.DbContexts;

namespace SeeSharp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>();

        return services;
    }
}

