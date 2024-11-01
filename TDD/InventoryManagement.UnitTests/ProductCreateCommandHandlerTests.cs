using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Products.Commands;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Domains.Events;
using Moq;

namespace InventoryManagement.UnitTests;

public class ProductCreateCommandHandlerTests
{
    private readonly Mock<IAppDbContext> _mockDbContext;
    private readonly ProductCreateCommandHandler _handler;

    public ProductCreateCommandHandlerTests()
    {
        _mockDbContext = new Mock<IAppDbContext>();
        _handler = new ProductCreateCommandHandler(_mockDbContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddProductAndRaiseProductCreatedEvent()
    {
        // Arrange
        var command = new ProductCreateCommand("Product Name", "Product Description", 100.0m, 10);

        var products = new List<Product>();
        _mockDbContext.Setup(db => db.Products.Add(It.IsAny<Product>())).Callback<Product>(p => products.Add(p));
        _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var productId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Single(products); // Verify one product was added
        var addedProduct = products.First();

        Assert.Equal("Product Name", addedProduct.Name);
        Assert.Equal("Product Description", addedProduct.Description);
        Assert.Equal(100.0m, addedProduct.Price);
        Assert.Equal(10, addedProduct.Stock);

        // Verify that ProductCreatedEvent is in DomainEvents
        Assert.Contains(addedProduct.DomainEvents, e => e is ProductCreatedEvent);
        
        // Verify SaveChangesAsync is called
        _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


}