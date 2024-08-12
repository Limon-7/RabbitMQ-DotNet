using ELK.Service.Domain.Services;
using ELK.Service.Infrastructure.Services;
using Nest;

namespace ELK.Service.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var uri = configuration["ELKConfig:Uri"];
        var defaultIndex = configuration["ELKConfig:Index"];
        
        var elkConfig = new ConnectionSettings(new Uri(uri)).PrettyJson().DefaultIndex(defaultIndex);
        services.AddSingleton(new ElasticClient(elkConfig));
        services.AddScoped(typeof(IElasticSearchService<>),typeof( ElasticSearchService<>));
        return services;
    }
}