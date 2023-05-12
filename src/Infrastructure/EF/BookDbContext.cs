using ELibrary_BookService.Domain.EF.Config;
using ELibrary_BookService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ELibrary_BookService.Domain.EF
{
    public class BookDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "bookService";
        public DbSet<Book> Books { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfig());
        }
    }
}
