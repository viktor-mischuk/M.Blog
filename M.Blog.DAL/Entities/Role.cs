

namespace M.Blog.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<User> Users { get; set; } = new();
    }
}
