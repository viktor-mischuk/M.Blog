using M.Blog.BLL.DTOs.PostDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostAPIController(IPostService postService) : ControllerBase
    {
        
        /// <summary>
        /// Create Post
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(NewPostViewModel vm)
        {

            HttpContext context = this.HttpContext;
            //Текущий пользователь
            var email = context.User.Claims.Where(c => c.Type == "userEmail").FirstOrDefault().Value;


            NewPostDTO postDto = new NewPostDTO()
            {
                Title = vm.Title,
                Content = vm.Content,
                Email = email,
                Tags = vm.Tags.Where(t => t.IsChecked == true).Select(s => s.Name).ToList(),
            };

            await postService.CreateAsync(postDto);
            return Ok("Статья добавлена");
        }


        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> Index()
        {
            List<PostViewModel> posts = new();

            var dtos = await postService.GetAllAsync();
            foreach (var dto in dtos)
            {
                PostViewModel vm = new()
                {
                    Title = dto.Title,
                    Tags = dto.Tags,
                };
                posts.Add(vm);
            }
            return Ok(posts);
        }


        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(PostViewModel vm)
        {


            var dto = new EditPostDTO()
            {
                NewContent = vm.Content
            };

            await postService.UpdateAsync(dto);
            return Ok("Статья обновлена");
        }


        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete( int id)
        {
            await postService.DeleteAsync(id);
            return Ok("Статья удалена");
        }

    }
}
