using M.Blog.BLL.DTOs.CommentDTO;
using M.Blog.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CommentAPIController(ICommentService commentService) : ControllerBase
    {

       /// <summary>
       /// Create comment
       /// </summary>
       /// <param name="content"></param>
       /// <param name="postId"></param>
       /// <param name="authorId"></param>
       /// <returns></returns>
       /// <exception cref="Exception"></exception>
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
            return Ok("Комментарий создан");
        }


        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await commentService.DeleteAsync(id);
            return Ok("Комментарий удален");
        }


        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns></returns>
        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await commentService.GetAllAsync();
            return Ok(dtos);
        }


        /// <summary>
        /// Get comment by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetComment")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await commentService.GetAsync(id);
            return Ok(dto);
        }


        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newContent"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, string newContent)
        {
            var dto = new UpdateCommentDTO()
            {
                Id = id,
                NewContent = newContent
            };

            await commentService.UpdateAsync(dto);
            return Ok("Комментарий обновлен");
        }
    }
}
