using M.Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace M.Blog.DAL
{
    public class AppContext(DbContextOptions<AppContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role[]
                {
                    new Role{Id=1, Name ="User"}
                });
            
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            //modelBuilder.Entity<User>().HasMany(u => u.Roles);

            base.OnModelCreating(modelBuilder);
        }
    }
}
