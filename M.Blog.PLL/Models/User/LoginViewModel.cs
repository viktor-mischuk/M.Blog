using System.ComponentModel.DataAnnotations;

namespace M.Blog.PLL.Models.User
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
