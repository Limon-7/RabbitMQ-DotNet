using Microsoft.AspNetCore.Mvc;

namespace HealthCheckWebhook.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthChecksController : ControllerBase
{
    private readonly ILogger<HealthChecksController> _logger;

    public HealthChecksController(ILogger<HealthChecksController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var response= await Task.FromResult(0);
        return Ok(new {Statue=true});
    }
    [HttpPost]
    [NonAction]
    // This the webhook endpoints
    public async Task<IActionResult> PostAsync()
    {
        var response= await Task.FromResult(0);
       return Ok(new {Statue=true});
    }
}
