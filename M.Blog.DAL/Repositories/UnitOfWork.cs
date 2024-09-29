using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;


namespace M.Blog.DAL.Repositories
{
    public class UnitOfWork(AppContext context) : IUnitOfWork
    {
        private UserRepository userRepository;
        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository is null) userRepository = new(context);
                return userRepository;
            }
        }

        private RoleRepository roleRepository;
        public IRepository<Role> RoleRepository
        {
            get
            {
                if (roleRepository is null) roleRepository = new(context);
                return roleRepository;
            }
        }

        private PostRepository postRepository;
        public IRepository<Post> PostRepository
        {
            get
            {
                if (postRepository is null) postRepository = new(context);
                return postRepository;
            }
        }

        private TagRepository tagRepository;
        public IRepository<Tag> TagRepository
        {
            get
            {
                if (tagRepository is null) tagRepository = new(context);
                return tagRepository;
            }
        }

        private CommentRepository commentRepository;
        public IRepository<Comment>CommentRepository
        {
            get
            {
                if (commentRepository is null) commentRepository = new(context);
                return commentRepository;
            }
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
