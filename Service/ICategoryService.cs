using DTOs;
using Entities;

namespace Service
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategories();
    }
}