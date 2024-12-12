

using System.Linq.Expressions;

namespace M.Blog.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(); // получение всех объектов
       // Task<T> Get(int id); // получение одного объекта по id
        Task<T> GetBy(Expression<Func<T, bool>> predicate); // получение одного объекта по выражению
        Task<IEnumerable<T>> GetManyBy(Expression<Func<T, bool>> predicate); // получение несколько объектов по выражению
        Task Create(T item); // создание объекта
        Task Update(T item); // обновление объекта
        Task Delete(T item); // удаление объекта по id
    }
}
