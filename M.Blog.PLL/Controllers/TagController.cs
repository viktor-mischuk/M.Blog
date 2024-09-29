using M.Blog.BLL.DTOs.TagDTO;
using M.Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{

    [ApiController]
    [Route("Tag")]
    public class TagController(ITagService tagService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name)
        {
            await tagService.CreateAsync(name);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await tagService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await tagService.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("Gettag")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await tagService.GetAsync(id);
            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, string newName)
        {
            var dto = new UpdateTagDTO()
            {
                Id = id,
                NewName = newName
            };

            await tagService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
