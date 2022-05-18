using System.Linq.Expressions;

namespace Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate);
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task Create(T t);
        Task Delete(Guid id);
        Task Edit(T t);
        Task<bool> CheckExistBy(Expression<Func<T, bool>> predicate);
    }
}