using Application.Models;

namespace Application.Services;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
    Task<IEnumerable<OrderResponse>> GetOrdersByCustomerAsync(Guid customerId);
}
