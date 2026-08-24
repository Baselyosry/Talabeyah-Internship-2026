namespace Domain.Models;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    public Product(Guid id, string name, string description, decimal price, int stockQuantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void Update(string name, string description, decimal price, int stockQuantity)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity > StockQuantity)
            throw new ArgumentException($"Not enough stock for product '{Name}'.");

        StockQuantity -= quantity;
    }
}
