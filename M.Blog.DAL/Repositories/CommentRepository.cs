using M.Blog.DAL.Entities;

namespace M.Blog.DAL.Repositories
{
    internal class CommentRepository : Repository<Comment>
    {
        public CommentRepository(AppContext context) : base(context) { }

    }
}
