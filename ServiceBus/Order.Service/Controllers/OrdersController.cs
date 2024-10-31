using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Service.Commands;

namespace Order.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IPublishEndpoint _publisher;
        private readonly IRequestClient<CheckOrderStatus> _client;
        private readonly IBus _bus;

        public OrdersController(ILogger<OrdersController> logger, IPublishEndpoint publisher,
            IRequestClient<CheckOrderStatus> client, IBus bus)
        {
            _logger = logger;
            _publisher = publisher;
            _client = client;
            _bus = bus;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateProductCommand([FromBody] CreateOrderCommand command)
        {
            await _publisher.Publish(new OrderPlaced(101, "Limon")); 
            await _bus.Send(new OrderPlaced(101, "Limon"),
                context =>
                {
                    context.Headers.Set("correlation-id", Guid.NewGuid());
                    context.Headers.Set("event-source", "OrderService");
                }); 
            return Ok("Successful");
        }

        [HttpPost]
        [Route("check-order-status")]
        public async Task<ActionResult> CheckOrderStatus([FromQuery] int orderId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Sending request to check order status for order ID: {OrderId}", orderId);
                var response = await _client
                    .GetResponse<OrderStatusResult>(
                        new CheckOrderStatus(orderId), cancellationToken,
                        timeout: RequestTimeout.After(s:30));
                return Ok(response.Message);
            }
            catch (RequestTimeoutException ex)
            {
                _logger.LogError(ex, "Timeout waiting for response for order ID: {OrderId}", orderId);
                return StatusCode(StatusCodes.Status504GatewayTimeout, "Timeout waiting for response");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking order status for order ID: {OrderId}", orderId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while checking order status");
            }
        }
    }
}