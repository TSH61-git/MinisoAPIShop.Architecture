using DTOs;
using Entities;

namespace Repository
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<bool> ProductHasOrdersAsync(int productId);
        Task<Product?> GetProductById(int id);
        Task<(List<Product> Items, int TotalCount)> GetProducts(int position, int skip, ProductSearchParams? parameters);
        Task<bool> UpdateProductAsync(Product product);
    }
}