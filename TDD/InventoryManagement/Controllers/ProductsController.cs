using InventoryManagement.Application.Products.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;

public class ProductsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> CreateProduct([FromBody] ProductCreateCommand createProductCommand)
    {
        var response = await Mediator.Send(createProductCommand);
        return response;
    }
}