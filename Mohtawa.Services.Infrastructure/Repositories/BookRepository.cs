using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Domain.Models.Entities;
using Mohtawa.Services.Infrastructure.Contexts;

namespace Mohtawa.Services.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>,IBookRepository
    {
        public BookRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
