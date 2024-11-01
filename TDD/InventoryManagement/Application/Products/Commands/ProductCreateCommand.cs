using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Domains.Events;
using MediatR;

namespace InventoryManagement.Application.Products.Commands;

public class ProductCreateCommand : IRequest<int>
{
    public ProductCreateCommand(string name, string description, decimal price, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, int>
{
    private readonly IAppDbContext _context;

    public ProductCreateCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock
        };

        product.DomainEvents.Add(new ProductCreatedEvent(product));

        _context.Products?.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}