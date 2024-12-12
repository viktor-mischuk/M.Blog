using M.Blog.BLL.DTOs.CommentDTO;
using M.Blog.BLL.DTOs.TagDTO;
using System.ComponentModel.DataAnnotations;

namespace M.Blog.PLL.Models.Post
{
    public class PostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public List<string> Tags { get; set; }
        [Required]
        public string NewComment { get; set; }
        [Required]
        public List<CommentDTO> Comments { get; set; }
    }
}
