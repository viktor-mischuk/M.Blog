

using M.Blog.BLL.DTOs.CommentDTO;

namespace M.Blog.BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateAsync(NewCommentDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCommentDTO dto);
        Task<IEnumerable<CommentDTO>> GetAllAsync();
        Task<CommentDTO> GetAsync(int id);

    }
}
