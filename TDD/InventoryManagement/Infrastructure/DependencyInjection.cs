using InventoryManagement.Application.Interfaces;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }
}