using InventoryManagement.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<Product>? Products { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}