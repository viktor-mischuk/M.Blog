using M.Blog.BLL.DTOs.UserDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{

    public class UserController(IUserService userService) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            NewUserDTO dto = new() 
            {
                UserName = vm.Name,
                Email = vm.Email,
                Password = vm.Password
            };
            await userService.CreateAsync(dto);
            return NoContent();
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
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
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var dto = await userService.GetByEmailAsync(email);
            UserViewModel vm = new()
            { 
                Email = dto.Email,
                Name = dto.Name,
                RoleNames = dto.RoleNames
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            UpdateUserDTO dto = new()
            {
                NewUserEmail = vm.Email,
                NewUserName = vm.Name,
                NewUserPassword = vm.Password
            };

            await userService.UpdateAsync(dto);
            return NoContent();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) 
            {
                throw new Exception();
            }
            
            var token = await userService.LoginAsync(vm.Email, vm.Password);

            HttpContext context = this.HttpContext;
            context.Response.Cookies.Append("some-cookies", token);

            return RedirectToAction("Index", "Post");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext context = this.HttpContext;
            context.Response.Cookies.Delete("some-cookies");
            return RedirectToAction("Index", "Home");

        }
    }
}
