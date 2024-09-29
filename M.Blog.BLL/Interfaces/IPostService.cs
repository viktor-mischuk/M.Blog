using M.Blog.BLL.DTOs.PostDTO;

namespace M.Blog.BLL.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> GetAsync(int id);
        Task<IEnumerable<PostDTO>> GetAllAsync();
        Task CreateAsync(NewPostDTO dto);
        Task UpdateAsync(EditPostDTO dto);
        Task DeleteAsync(int id);
    }
}
