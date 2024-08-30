using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckWebhook.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
        services.AddHealthChecks()
            .AddRedis(configuration["CacheSettings:ConnectionString"]!, "Redis Health", HealthStatus.Degraded);
        return services;
    }
}