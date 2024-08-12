using ELK.Service.Domain.Entities;
using ELK.Service.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELK.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly IElasticSearchService<Catalog> _elasticSearchService;
    private readonly ILogger<CatalogsController> _logger;

    public CatalogsController(IElasticSearchService<Catalog> elasticSearchService, ILogger<CatalogsController> logger)
    {
        _elasticSearchService = elasticSearchService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string keyword)
    {
        _logger.LogInformation($"Requesting Search: {keyword}");
        var response = await _elasticSearchService.GetAllDocumentsAsync(keyword);
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var response = await _elasticSearchService.GetDocumentByIdAsync(id);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Catalog request)
    {
        var response = await _elasticSearchService.CreateDocumentAsync(request);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Catalog request)
    {
        var response = await _elasticSearchService.UpdateDocumentAsync(request);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _elasticSearchService.DeleteDocumentAsync(id);
        return Ok(response);
    }
}
