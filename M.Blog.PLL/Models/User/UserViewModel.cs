using System.ComponentModel.DataAnnotations;

namespace M.Blog.PLL.Models.User
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public List<string> RoleNames { get; set; }
    }
}
