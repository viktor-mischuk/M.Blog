using M.Blog.DAL.Entities;


namespace M.Blog.DAL.Repositories
{
    internal class RoleRepository : Repository<Role>
    {
        public RoleRepository(AppContext context) : base(context) { }
 
    }
}
