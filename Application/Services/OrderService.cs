using Application.Interfaces;
using Application.Models;
using Domain.Models;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (request.CustomerId == Guid.Empty)
            throw new ArgumentException("Customer ID is required.");

        if (request.Items is null || request.Items.Count == 0)
            throw new ArgumentException("Order must contain at least one item.");

        var customer = await _userRepository.GetByIdAsync(request.CustomerId);
        if (customer is null)
            throw new ArgumentException($"Customer with ID '{request.CustomerId}' was not found.");

        var order = new Order(request.CustomerId);

        foreach (var item in request.Items)
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("Item quantity must be greater than zero.");

            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product is null)
                throw new ArgumentException($"Product with ID '{item.ProductId}' was not found.");

            product.ReduceStock(item.Quantity);
            await _productRepository.UpdateAsync(product);

            var orderProduct = new OrderProduct(product.Id, item.Quantity, product.Price);
            order.AddOrderProduct(orderProduct);
        }

        await _orderRepository.AddAsync(order);

        return MapToResponse(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersByCustomerAsync(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer ID is required.");

        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
        return orders.Select(MapToResponse);
    }

    private static OrderResponse MapToResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            TotalPrice = order.TotalPrice,
            Status = order.Status.ToString(),
            CreatedAt = order.CreatedAt,
            Items = order.OrderProducts.Select(op => new OrderProductResponse
            {
                Id = op.Id,
                ProductId = op.ProductId,
                Quantity = op.Quantity,
                ItemPrice = op.ItemPrice,
                TotalPrice = op.TotalPrice
            }).ToList()
        };
    }
}
