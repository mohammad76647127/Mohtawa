using Mohtawa.Services.Domain.Models;

namespace Mohtawa.Services.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> IsExists(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    }
}
