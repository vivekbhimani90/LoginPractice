using System.Linq.Expressions;

namespace LoginPractice.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? fillter = null, string includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> fillter = null, bool tracked = true, string includeProperties = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
