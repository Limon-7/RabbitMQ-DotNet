using System.ComponentModel.DataAnnotations;
using InventoryManagement.Domains.Common;

namespace InventoryManagement.Domains.Entities;

public class Product : AuditableEntity, IHasDomainEvent
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new();
}