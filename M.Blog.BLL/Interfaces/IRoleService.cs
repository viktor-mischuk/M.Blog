
using M.Blog.BLL.DTOs.RoleDTO;

namespace M.Blog.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> GetAsync(int id);
        Task<RoleDTO> GetByNameAsync(string name);
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task CreateAsync(NewRoleDTO dto);
        Task UpdateAsync(EditRoleDTO dto);
        Task DeleteAsync(int id);
    }
}
