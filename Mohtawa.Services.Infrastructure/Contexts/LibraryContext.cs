using Microsoft.EntityFrameworkCore;
using Mohtawa.Services.Domain.Models.Entities;

namespace Mohtawa.Services.Infrastructure.Contexts
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
