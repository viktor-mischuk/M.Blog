using M.Blog.BLL.DTOs.UserDTO;
using M.Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController(IUserService userService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name, string email, string password)
        {
            NewUserDTO dto = new() 
            {
                UserName = name,
                Email = email,
                Password = password
            };
            await userService.CreateAsync(dto);
            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await userService.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await userService.GetAllAsync();

            return Ok(dtos);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await userService.GetAsync(id);
            return Ok(result);
        }


       
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int? id, string? newName, string? newEmail, string? newPassword)
        {
            UpdateUserDTO dto = new()
            {
                NewUserEmail = newEmail,
                NewUserName = newName,
                NewUserPassword = newPassword
            };

            await userService.UpdateAsync(dto);
            return NoContent();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await userService.LoginAsync(dto.Email, dto.Password);

            HttpContext context = this.HttpContext;
            context.Response.Cookies.Append("some-cookies", token);

            return Ok(token);
        }
    }
}
