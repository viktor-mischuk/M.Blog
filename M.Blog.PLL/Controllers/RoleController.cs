using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{
    public class RoleController(IRoleService roleService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RoleViewModel> roles = new ();
            var dtos = await roleService.GetAllAsync();
            if (ModelState.IsValid)
            {
                foreach (var dto in dtos)
                {
                    RoleViewModel vm = new()
                    {
                        Name = dto.Name,
                        Description = dto.Description,
                    };
                    roles.Add(vm);
                }
                return View(roles);
            }
            throw new Exception();
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string name)
        {
            var dto = await roleService.GetByNameAsync(name);
            RoleViewModel vm = new()
            {
                Name = dto.Name,
                Description = dto.Description,
            };
            
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(RoleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            return NoContent();
        }
    }

}
