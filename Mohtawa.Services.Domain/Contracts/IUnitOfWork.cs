using Mohtawa.Services.Domain.Models;

namespace Mohtawa.Services.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Save();
    }
}
