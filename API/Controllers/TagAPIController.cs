using M.Blog.BLL.DTOs.TagDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{
    ///
    [ApiController]
    [Route("[controller]")]
    public class TagAPIController(ITagService tagService) : ControllerBase
    {
        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagViewModel>>> Index()
        {
            List<TagViewModel> tags = new();
            var dtos = await tagService.GetAllAsync();
            foreach (var dto in dtos)
            {
                TagViewModel vm = new()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                };
                tags.Add(vm);
            }
            return Ok(tags);
        }

        /// <summary>
        /// Create tag
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost("name")]
        public async Task<IActionResult> Create(string name)
        {
            await tagService.CreateAsync(name);
            return Ok("Тег создан");
        }


        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await tagService.DeleteAsync(id);
            return Ok("Тег удален");
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("{id}/{newName}")]
        public async Task<IActionResult> Update(int id, string name)
        {

            var dto = new UpdateTagDTO()
            {
                Id = id,
                NewName = name
            };

            await tagService.UpdateAsync(dto);
            return Ok();
        }
    }
}
