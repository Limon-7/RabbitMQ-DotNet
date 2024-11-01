using System.Reflection;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domains.Common;
using InventoryManagement.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventService domainEventService,
        IDateTime dateTime) : base(options)
    {
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public DbSet<Product>? Products { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "_currentUserService.UserId";
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "_currentUserService.UserId";
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        // add outbox pattern

        await DispatchEvents(events);

        return result;
    }


    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}