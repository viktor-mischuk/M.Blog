

namespace M.Blog.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
       

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public User User { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();


    }
}
