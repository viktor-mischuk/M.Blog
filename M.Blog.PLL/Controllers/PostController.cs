using M.Blog.BLL.DTOs.PostDTO;
using M.Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{
    [ApiController]
    [Route("Post")]
    public class PostController(IPostService postService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string title, string content, int authorID)
        {
            NewPostDTO postDto = new NewPostDTO()
            { 
                Title = title,
                Content = content,
                AuthorId = authorID
            };
            
            await postService.CreateAsync(postDto);
            return NoContent();
        }
        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await postService.GetAllAsync();
            return Ok(dtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await postService.GetAsync(id);
            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, string newContent)
        {
            var dto = new EditPostDTO()
            {
                PostId = id,
                NewContent = newContent
            };
            
            await postService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await postService.DeleteAsync(id);
            return NoContent();
        }

    }
}
