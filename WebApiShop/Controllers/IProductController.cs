using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IProductController
    {
        Task<IActionResult> CreateProduct([FromBody] Product product);
        Task<IActionResult> DeleteProduct(int id);
        Task<ActionResult<PageResponseDTO<ProductDTO>>> Get(int position, int skip, [FromQuery] ProductSearchParams parameters);
        Task<IActionResult> GetProductById(int id);
        Task<IActionResult> UpdateProduct(int id, [FromBody] Product product);
    }
}