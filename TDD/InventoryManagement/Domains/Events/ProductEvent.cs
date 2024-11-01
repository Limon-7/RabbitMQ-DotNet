using InventoryManagement.Domains.Common;
using InventoryManagement.Domains.Entities;

namespace InventoryManagement.Domains.Events;

public class ProductCreatedEvent : DomainEvent
{
    public ProductCreatedEvent(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}

public class ProductUpdatedEvent : DomainEvent
{
    public ProductUpdatedEvent(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}

public class ProductDeletedEvent : DomainEvent
{
    public ProductDeletedEvent(int productId)
    {
        ProductId = productId;
    }

    public int ProductId { get; }
}