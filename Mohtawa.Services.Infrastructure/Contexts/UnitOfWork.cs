using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Domain.Models;
using Mohtawa.Services.Infrastructure.Repositories;
using System.Collections;


namespace Mohtawa.Services.Infrastructure.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _libraryContext;
        private Hashtable _repositories;


        public UnitOfWork(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<int> Save()
        {
            return await _libraryContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _libraryContext.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            _repositories ??= [];

            var Type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _libraryContext);
                _repositories.Add(Type, repositoryInstance);
            }

            return _repositories[Type] as IGenericRepository<TEntity>;
        }
    }

}
