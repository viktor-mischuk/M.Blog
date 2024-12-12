

namespace M.Blog.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
    }
}
