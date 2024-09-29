

namespace M.Blog.BLL.DTOs.UserDTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; } = 0!;
        public string? NewUserName { get; set; }
        public string? NewUserEmail { get; set; }
        public string? NewUserPassword { get; set; }
    }
}
