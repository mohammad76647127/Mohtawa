using Mohtawa.Services.Domain.Models.Entities;

namespace Mohtawa.Services.Domain.Contracts
{
    public interface IUserRepository :IGenericRepository<User>
    {
        Task<User?> GetUserByUserName(string userName);
    }
}
