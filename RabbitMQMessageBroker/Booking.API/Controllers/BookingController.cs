using System.Collections.Generic;
using System.Text.Json;
using Booking.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Booking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMessageProducer _producer;

        private readonly List<Entities.Booking> _bookings = new ();

        public BookingController(ILogger<BookingController> logger, IMessageProducer producer)
        {
            _logger = logger;
            _producer = producer;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            return Ok(_bookings);
        }
        
        [HttpPost]
        public IActionResult PostAsync([FromBody] Entities.Booking request)
        {
            _logger.LogInformation($"Requesting a Booking: {JsonSerializer.Serialize(request)}");
            _bookings.Add(request);
            _producer.PublishMessage(request);
            return Ok(new {Status="Success"});
        }
        
    }
}
