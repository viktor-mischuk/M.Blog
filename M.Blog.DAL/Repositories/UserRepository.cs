using M.Blog.DAL.Entities;


namespace M.Blog.DAL.Repositories
{
    internal class UserRepository : Repository<User>
    {
        public UserRepository(AppContext context) : base(context) { }
 
    }
}
