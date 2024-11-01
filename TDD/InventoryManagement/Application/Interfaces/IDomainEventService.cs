using InventoryManagement.Domains.Common;

namespace InventoryManagement.Application.Interfaces;

// Define an event bus interface that will handle publishing domain events.
public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}