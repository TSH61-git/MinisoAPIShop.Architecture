using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIShop.Controllers
{
    public interface IUserController
    {
        void Delete(int id);
        Task<IEnumerable<string>> Get();
        Task<ActionResult<UserReadDTO>> Get(int id);
        Task<ActionResult<UserReadDTO>> Login([FromBody] UserLoginDTO loginDto);
        Task<ActionResult<UserReadDTO>> Post([FromBody] UserRegisterDTO userRegisterDto); 
        Task<IActionResult> Put(int id, [FromBody] UserUpdateDTO userUpdateDto);
    }

}