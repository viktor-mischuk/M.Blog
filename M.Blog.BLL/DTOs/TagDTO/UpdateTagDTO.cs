using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M.Blog.BLL.DTOs.TagDTO
{
    public class UpdateTagDTO
    {
        public int Id { get; set; } = 0!;
        public string? NewName { get; set; } = null!;
    }
}
