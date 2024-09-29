using M.Blog.DAL.Entities;


namespace M.Blog.BLL.DTOs.TagDTO
{
    public class TagDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public List<Tag>? Posts { get; set; }
    }
}
