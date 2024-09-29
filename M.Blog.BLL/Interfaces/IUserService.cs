using M.Blog.BLL.DTOs.UserDTO;

namespace M.Blog.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(NewUserDTO dto); // Register user
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateUserDTO dto);
        Task<string> LoginAsync(string email, string passWord); //Login user
        Task<UserDTO> GetAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        
        
    }
}
