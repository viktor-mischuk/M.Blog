

namespace M.Blog.BLL.DTOs.PostDTO
{
    public class NewPostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public List<string> Tags { get; set; }
    }
}
