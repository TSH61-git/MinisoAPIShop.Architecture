using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IBranchController
    {
        void Delete(int id);
        string Get(int id);
        Task<ActionResult<IEnumerable<BranchDTO>>> Get([FromQuery] string? query);
        void Post([FromBody] string value);
        void Put(int id, [FromBody] string value);
    }
}