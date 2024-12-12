using System.ComponentModel.DataAnnotations;

namespace M.Blog.PLL.Models.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name {  get; set; }   
    }
}
