using M.Blog.BLL.Interfaces;
using M.Blog.PLL.Models.Tag;
using Microsoft.AspNetCore.Mvc;

namespace M.Blog.PLL.Controllers.Components
{
    //[ViewComponent]
    public class TagCloudComponent(ITagService tagService)
    {

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    List<TagViewModel> viewModels = new List<TagViewModel>();
        //    var dtos = await tagService.GetAllAsync();
        //    foreach (var d in dtos)
        //    {
        //        var vm = new TagViewModel()
        //        {
        //            Id = d.Id,
        //            Name = d.Name
        //        };
        //        viewModels.Add(vm);
        //    }
        //    return View("TagCloud", viewModels);
        //}
    }
}
