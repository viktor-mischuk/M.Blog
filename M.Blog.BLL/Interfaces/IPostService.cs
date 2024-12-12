using M.Blog.BLL.DTOs.PostDTO;
using M.Blog.BLL.DTOs.TagDTO;

namespace M.Blog.BLL.Interfaces
{
    public interface IPostService
    {
        Task<List<string>> GetAllTags();
        Task<PostDTO> GetAsync(int id);
        Task<PostDTO> GetByTitleAsync(string title);
        Task<IEnumerable<PostDTO>> GetAllAsync();
        Task CreateAsync(NewPostDTO dto);
        Task UpdateAsync(EditPostDTO dto);
        Task DeleteAsync(int id);
    }
}
