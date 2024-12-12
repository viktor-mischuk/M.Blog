using M.Blog.BLL.DTOs.TagDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers
{


    public class TagController(ITagService tagService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TagViewModel> tags = new();
            var dtos = await tagService.GetAllAsync();
            foreach(var dto in dtos)
            {
                TagViewModel vm = new() 
                {
                    Id= dto.Id,
                    Name = dto.Name,
                };
                tags.Add(vm);
            }
            return View(tags);
        }


        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TagViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            await tagService.CreateAsync(vm.Name);
            return RedirectToAction("Index", "Tag");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await tagService.DeleteAsync(id);
            return RedirectToAction("Index", "Tag");
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await tagService.GetAsync(id);
            return View(dto);
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {


            var dto = await tagService.GetAsync(id);
            TagViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TagViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var dto = new UpdateTagDTO()
            {
                Id = vm.Id,
                NewName = vm.Name
            };

            await tagService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
