using InventoryManagement.Domains.Entities;

namespace InventoryManagement.Domains.Interfaces;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task<IEnumerable<Product>> GetAllAsync();
}