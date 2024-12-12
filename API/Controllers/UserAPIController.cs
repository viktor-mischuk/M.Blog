using M.Blog.BLL.DTOs.UserDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAPIController(IUserService userService) : ControllerBase
    {

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("{name}/{email}/{paswword}")]
        public async Task<IActionResult> Create(string name, string email, string password)
        {
            NewUserDTO dto = new() 
            {
                UserName = name,
                Email = email,
                Password = password
            };
            await userService.CreateAsync(dto);
            return Ok("Учетная запись создана");
        }


        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.DeleteAsync(id);
            return Ok("Учетная запись удалена");
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Index()
        {
            List<UserViewModel> users = new ();
            var dtos = await userService.GetAllAsync();
            foreach(var dto in dtos)
            {
                UserViewModel vm = new()
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    RoleNames = dto.RoleNames,
                };
                users.Add(vm);
            }

            var u = this.HttpContext.User.Identity;
            return Ok(users);
        }

        /// <summary>
        /// Get user by e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var dto = await userService.GetByEmailAsync(email);
            UserViewModel vm = new()
            {
                Email = dto.Email,
                Name = dto.Name,
                RoleNames = dto.RoleNames
            };
            return Ok(vm);
        }

        /// <summary>
        /// Update user's data
        /// </summary>
        /// <param name="newEmail"></param>
        /// <param name="newName"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut("{newEmail}/{newName}/{newPassword}")]
        public async Task<IActionResult> Update(string newEmail, string newName, string newPassword)
        {
            UpdateUserDTO dto = new()
            {
                NewUserEmail = newEmail,
                NewUserName = newName,
                NewUserPassword = newPassword
            };

            await userService.UpdateAsync(dto);
            return Ok("Данные пользователя обновлены");
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password)
        {


            var token = await userService.LoginAsync(email, password);

            HttpContext context = this.HttpContext;
            context.Response.Cookies.Append("some-cookies", token);

            return Ok();
        }

    }
}
