using M.Blog.DAL.Entities;

namespace M.Blog.DAL.Repositories
{
    public class TagRepository: Repository<Tag>
    {
        public TagRepository(AppContext context) : base(context) { }
  
    }
}
