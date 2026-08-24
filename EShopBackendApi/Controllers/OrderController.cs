using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var order = await _orderService.CreateOrderAsync(request);
        return Created($"/api/Order/{order.Id}", order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
        return Ok(orders);
    }
}
