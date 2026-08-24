using Application.Models;
using Domain.Models;

namespace Application.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> CreateAsync(ProductRequest request);
    Task<Product?> UpdateAsync(Guid id, ProductRequest request);
}
