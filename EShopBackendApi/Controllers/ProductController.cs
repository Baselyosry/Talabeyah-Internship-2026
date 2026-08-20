using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest request)
    {
        var product = await _productService.CreateAsync(request);
        return Created($"/api/Product/{product.Id}", product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequest request)
    {
        var product = await _productService.UpdateAsync(id, request);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
}
