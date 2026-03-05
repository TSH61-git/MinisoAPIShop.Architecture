using DTOs;
using Entities;

namespace Service
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<Product?> GetProductById(int id);
        Task<PageResponseDTO<ProductDTO>> GetProducts(int position, int skip, ProductSearchParams parameters);
        Task<bool> UpdateProductAsync(Product product);
    }
}