using M.Blog.BLL.DTOs.RoleDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleAPIController(IRoleService roleService) : ControllerBase
    {
        
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewModel>>> Index()
        {
            List<RoleViewModel> roles = new();
            var dtos = await roleService.GetAllAsync();

                foreach (var dto in dtos)
                {
                    RoleViewModel vm = new()
                    {
                        Name = dto.Name,
                        Description = dto.Description,
                    };
                    roles.Add(vm);
                }
                return Ok(roles);

        }

        /// <summary>
        /// Get role by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetRole(string name)
        {
            var dto = await roleService.GetByNameAsync(name);
            RoleViewModel vm = new()
            {
                Name = dto.Name,
                Description = dto.Description,
            };

            return Ok(vm);
        }

  
        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("{name}/{description}")]
        public async Task<IActionResult> Create(string name, string description)
        {
            NewRoleDTO roleDTO = new() 
            {
                Name = name,
                Description = description
            };

            await roleService.CreateAsync(roleDTO);
            return Ok("Роль создана");
        }


        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await roleService.DeleteAsync(id);
            return Ok("Роль удалена");
        }
    }

}
