namespace Application.Models;

public class OrderProductResponse
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal ItemPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
