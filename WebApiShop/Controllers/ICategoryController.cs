using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface ICategoryController
    {
        void Delete(int id);
        Task<ActionResult<IEnumerable<List<CategoryDTO>>>> Get();
        string Get(int id);
        void Post([FromBody] string value);
        void Put(int id, [FromBody] string value);
    }
}