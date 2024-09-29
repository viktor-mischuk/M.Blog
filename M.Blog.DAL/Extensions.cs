using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;
using M.Blog.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace M.Blog.DAL
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IRepository<User>, UserRepository>();
            //serviceCollection.AddScoped<IRepository<Role>, RoleRepository>();
            //serviceCollection.AddScoped<IRepository<Post>, PostRepository>();
            //serviceCollection.AddScoped<IRepository<Comment>, CommentRepository>();
            //serviceCollection.AddScoped<IRepository<Tag>, TagRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            
            serviceCollection.AddDbContext<AppContext>(options =>
            {
                options.UseSqlite("DataSource=BlogDb.db");
            });
            
            return serviceCollection;
        }
    }
}
