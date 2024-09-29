using M.Blog.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace M.Blog.DAL.Repositories
{
    public class Repository<T>(AppContext db) : IRepository<T> where T: class
    {
        public DbSet<T> Set { get; private set; } = db.Set<T>();

        public async Task Create(T item)
        {
            await Set.AddAsync(item);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Set.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetManyBy(Expression<Func<T, bool>> predicate)
        {
            return await Set.Where(predicate).ToListAsync();
        }
        public async Task<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return await Set.Where(predicate).FirstOrDefaultAsync();
        }
        public Task Update(T item)
        {
            Set.Update(item);
            return Task.CompletedTask;
        }
        public Task Delete(T item)
        {
            Set.Remove(item);
            return Task.CompletedTask;
        }



   

  

  





   
    }
}
