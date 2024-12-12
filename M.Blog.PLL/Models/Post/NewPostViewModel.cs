using M.Blog.BLL.DTOs.CommentDTO;
using System.ComponentModel.DataAnnotations;

namespace M.Blog.PLL.Models.Post
{
    public class NewPostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public List<TagOption> Tags { get; set; }
    }
    public class TagOption
    {
        [Required]
        public bool IsChecked { get; set; }
        [Required]
        public string Name {  get; set; }
    }
}
