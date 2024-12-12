
using M.Blog.DAL.Entities;

namespace M.Blog.BLL.DTOs.PostDTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public List<CommentDTO.CommentDTO> Comments { get; set; }
    }
}
