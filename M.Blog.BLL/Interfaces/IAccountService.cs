

namespace M.Blog.DAL.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(string name, string email, string passWord);
        Task<string> LoginAsync(string email, string passWord);
    }
}
