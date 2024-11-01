using InventoryManagement.Application.Models;
using InventoryManagement.Domains.Events;
using MediatR;

namespace InventoryManagement.Application.Products.EventHandlers;

// publish the event
public class ProductCreatedEventHandler : INotificationHandler<DomainEventNotification<ProductCreatedEvent>>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<ProductCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}