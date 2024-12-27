using Microsoft.EntityFrameworkCore;
using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Domain.Models.Entities;
using Mohtawa.Services.Infrastructure.Contexts;

namespace Mohtawa.Services.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LibraryContext _libraryContext;
        public UserRepository(LibraryContext libraryContext) : base(libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _libraryContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
