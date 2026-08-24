namespace Domain.Models;

public class Order
{
    public int Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal TotalPrice { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderProduct> OrderProducts { get; private set; } = new();

    private Order() { }

    public Order(Guid customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        TotalPrice = 0;
    }

    public void AddOrderProduct(OrderProduct orderProduct)
    {
        OrderProducts.Add(orderProduct);
        TotalPrice += orderProduct.TotalPrice;
    }
}
