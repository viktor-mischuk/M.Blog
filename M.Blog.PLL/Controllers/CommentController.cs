using M.Blog.BLL.DTOs.CommentDTO;
using M.Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{

    [ApiController]
    [Route("Comment")]
    public class CommentController(ICommentService commentService) : Controller
    {
        
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string content, int postId, int authorId)
        {
            NewCommentDTO newCommentDTO = new() 
            {
                Content = content,
                PostId = postId,
                AuthorId = authorId
            };
            await commentService.CreateAsync(newCommentDTO);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await commentService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await commentService.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("GetComment")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await commentService.GetAsync(id);
            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, string newContent)
        {
            var dto = new UpdateCommentDTO()
            {
                Id = id,
                NewContent = newContent
            };

            await commentService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
