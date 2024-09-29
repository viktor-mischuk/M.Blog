using M.Blog.DAL.Entities;

namespace M.Blog.BLL.Interfaces
{
    public interface IJWTService
    {
        Task<string> GenerateToken(User user);
    }
}
