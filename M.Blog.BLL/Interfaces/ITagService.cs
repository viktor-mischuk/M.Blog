


using M.Blog.BLL.DTOs.TagDTO;

namespace M.Blog.BLL.Interfaces
{
    public interface ITagService
    {
        Task CreateAsync(string Name);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateTagDTO dto);
        Task<IEnumerable<TagDTO>> GetAllAsync();
        Task<TagDTO> GetAsync(int id);
    }
}
