using Application.Interfaces;
using Application.Models;
using Domain.Models;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Product?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    public async Task<Product> CreateAsync(ProductRequest request)
    {
        Validate(request);

        var product = new Product(Guid.NewGuid(), request.Name, request.Description,
            request.Price, request.StockQuantity);

        await _repository.AddAsync(product);
        return product;
    }

    public async Task<Product?> UpdateAsync(Guid id, ProductRequest request)
    {
        Validate(request);

        var product = await _repository.GetByIdAsync(id);
        if (product is null)
            return null;

        product.Update(request.Name, request.Description, request.Price, request.StockQuantity);

        await _repository.UpdateAsync(product);
        return product;
    }

    private void Validate(ProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Product name is required.");

        if (request.Price < 0)
            throw new ArgumentException("Price cannot be negative.");

        if (request.StockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.");
    }
}
