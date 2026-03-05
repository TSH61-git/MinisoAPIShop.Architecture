using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IPasswordController
    {
        IActionResult CheckPassword([FromBody] Password password);
    }
}