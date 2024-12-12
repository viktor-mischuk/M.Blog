

namespace M.Blog.BLL.DTOs.CommentDTO
{
    public class CommentDTO
    {
        public int? Id { get; set; }
        public string? PostTitle { get; set; }
        public string? AuthorName { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
