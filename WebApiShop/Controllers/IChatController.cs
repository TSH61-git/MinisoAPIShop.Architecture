using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IChatController
    {
        Task<IActionResult> Post([FromBody] ChatRequest req);
    }
}