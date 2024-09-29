

namespace M.Blog.BLL.DTOs.CommentDTO
{
    public class NewCommentDTO
    {
        public string? Content { get; set; } = null!;
        public int PostId { get; set; } = 0!;
        public int AuthorId { get; set; } = 0!;
    }
}
