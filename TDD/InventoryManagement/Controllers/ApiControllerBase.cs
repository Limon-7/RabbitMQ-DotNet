using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    public ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}