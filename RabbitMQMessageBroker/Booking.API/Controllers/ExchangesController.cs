using System.Collections.Generic;
using System.Text.Json;
using Booking.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Booking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangesController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly ExchangeProducer _producer;

        private readonly List<Entities.Booking> _bookings = new ();

        public ExchangesController(ILogger<BookingController> logger, ExchangeProducer producer)
        {
            _logger = logger;
            _producer = producer;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            return Ok(_bookings);
        }

        [HttpPost("fanout-exchange")]
        public IActionResult PostFanExchangeAsync([FromBody] Entities.Booking request)
        {
            _logger.LogInformation($"Fan-exchange: {JsonSerializer.Serialize(request)}");
            _bookings.Add(request);
            _producer.PublishFanOutExchangeMessage(request);
            return Ok(new {Status="Success"});
        }
        
        [HttpPost("route-exchange")]
        public IActionResult PostRouteExchangeAsync([FromBody] Entities.Booking request)
        {
            _logger.LogInformation($"Fan-exchange: {JsonSerializer.Serialize(request)}");
            _bookings.Add(request);
            _producer.PublishRoutingExchangeMessage(request);
            return Ok(new {Status="Success"});
        }
        
        [HttpPost("direct-exchange")]
        public IActionResult PostDirectExchangeAsync([FromBody] Entities.Booking request)
        {
            _logger.LogInformation($"Direct-exchange: {JsonSerializer.Serialize(request)}");
            _bookings.Add(request);
            _producer.PublishDirectExchangeMessage(request);
            return Ok(new {Status="Success"});
        }
        
        [HttpPost("topic-exchange")]
        public IActionResult PostTopicExchangeAsync([FromBody] Entities.Booking request)
        {
            _logger.LogInformation($"Topic-exchange: {JsonSerializer.Serialize(request)}");
            _bookings.Add(request);
            _producer.PublishTopicExchangeMessage(request);
            return Ok(new {Status="Success"});
        }
        
    }
}
