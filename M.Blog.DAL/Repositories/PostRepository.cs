using M.Blog.DAL.Entities;


namespace M.Blog.DAL.Repositories
{
    public class PostRepository : Repository<Post>
    {

        public PostRepository(AppContext context) : base(context) { }

    }
}
