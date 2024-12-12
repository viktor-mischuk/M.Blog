using M.Blog.BLL.DTOs.PostDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{

    public class PostController(IPostService postService) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var tags = await postService.GetAllTags();

            NewPostViewModel vm = new()
            {
                Tags = tags.Select(s => 
                new TagOption() 
                { 
                    Name = s,
                    IsChecked = false,
                }).ToList(),
           
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewPostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

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
            return RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPost(string postTitle)
        {
            var dto = await postService.GetByTitleAsync(postTitle);
            PostViewModel vm = new()
            {
                Title= dto.Title,
                Tags = dto.Tags,
                Content = dto.Content,
                Author = dto.Author,
                Comments = dto.Comments,
                
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var dto = new EditPostDTO()
            {
                NewContent = vm.Content
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
