using DTOs;
using Entities;
using Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase, IPasswordController
    {
        IPasswordService _iPasswordService;

        public PasswordController(IPasswordService passwordService)
        {
            _iPasswordService = passwordService;
        }

        [HttpPost]
        public IActionResult CheckPassword([FromBody] Password password)
        {
            Password resPassword = _iPasswordService.CheckPasswordStrong(password);
            if (resPassword == null)
                return NoContent();
            return Ok(resPassword);
        }
    }
}
