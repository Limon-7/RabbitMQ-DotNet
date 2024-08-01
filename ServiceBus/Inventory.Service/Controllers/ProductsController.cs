using System.Threading.Tasks;
using Contracts;
using Inventory.Service.Commands;
using MassTransit;
using MassTransit.RabbitMqTransport.Integration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IPublishEndpoint _publisher;

        public ProductsController(ILogger<ProductsController> logger, IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public Task<string> CreateProductCommand([FromBody] CreateProductCommand command)
        {
            _publisher.Publish<ProductCreationPlaced>(new ProductCreationPlaced(command.Id, command.Code,
                command.ProductName));
            return Task.FromResult("Successful");
        }
    }
}