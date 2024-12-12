using System.ComponentModel.DataAnnotations;

namespace M.Blog.PLL.Models.Role
{
    public class RoleViewModel
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Description { get; set; }
    }
}
