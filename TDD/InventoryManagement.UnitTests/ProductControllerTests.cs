using System.Reflection;
using InventoryManagement.Application.Products.Commands;
using InventoryManagement.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryManagement.UnitTests;

public class ProductControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<ISender> _mockSender;
    
        public ProductControllerTests()
        {
            _mockSender = new Mock<ISender>();
            _controller = new ProductsController();

            // Use reflection to set the private _mediator field
            var mediatorField = typeof(ApiControllerBase)
                .GetField("_mediator", BindingFlags.NonPublic | BindingFlags.Instance);
            mediatorField!.SetValue(_controller, _mockSender.Object);
        }

        [Fact]
        public async Task CreateProduct_ReturnsTrue()
        {
            // Arrange
            var createProductCommand = new ProductCreateCommand("New Product","niceone",30,10 );
            var expectedResponse = 1;

            _mockSender
                .Setup(m => m.Send(createProductCommand, default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateProduct(createProductCommand);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okResult = Assert.IsType<int>(actionResult.Value);
            Assert.Equal(expectedResponse, okResult);
        }
    }