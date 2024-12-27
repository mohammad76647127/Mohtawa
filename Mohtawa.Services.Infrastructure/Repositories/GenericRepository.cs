using Microsoft.EntityFrameworkCore;
using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Domain.Models;
using Mohtawa.Services.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace Mohtawa.Services.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly LibraryContext _libraryContext;

        public GenericRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        #region Read Operations
        public async Task<List<T>> GetAllAsync()
        {
            return await _libraryContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _libraryContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExists(Expression<Func<T, bool>> expression)
        {
            return await _libraryContext.Set<T>().AnyAsync(expression);
        }
        #endregion

        #region Write Operations
        public async Task AddAsync(T entity)
        {
            await _libraryContext.AddAsync<T>(entity);
        }
        public void Update(T entity)
        {
            //_libraryContext.Attach<T>(entity);
            //_libraryContext.Entry(entity).State = EntityState.Modified;
            _libraryContext.Update(entity);
        }
        public void Delete(T entity)
        {
            _libraryContext.Set<T>().Remove(entity);
        }
        #endregion

    }
}
