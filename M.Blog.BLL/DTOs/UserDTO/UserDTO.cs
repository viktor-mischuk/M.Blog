

using M.Blog.DAL.Entities;

namespace M.Blog.BLL.DTOs.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
