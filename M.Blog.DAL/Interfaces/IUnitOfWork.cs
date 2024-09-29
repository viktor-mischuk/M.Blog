using M.Blog.DAL.Entities;

namespace M.Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<Post> PostRepository { get; }
        IRepository<Tag> TagRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        Task Save();
    }
}
