namespace Application.Models;

public class OrderResponse
{
    public int Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderProductResponse> Items { get; set; } = new();
}
