namespace Domain.Models;

public class OrderProduct
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal ItemPrice { get; private set; }
    public decimal TotalPrice { get; private set; }

    private OrderProduct() { }

    public OrderProduct(Guid productId, int quantity, decimal itemPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        ItemPrice = itemPrice;
        TotalPrice = quantity * itemPrice;
    }
}
