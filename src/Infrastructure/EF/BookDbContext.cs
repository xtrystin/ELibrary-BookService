using ELibrary_BookService.Infrastructure.EF.Config;
using ELibrary_BookService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ELibrary_BookService.Infrastructure.EF
{
    public class BookDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "bookService";
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfig());
            KeepSingularTableName(modelBuilder);
        }

        private void KeepSingularTableName(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Author>().ToTable("Author");
        }
    }
}
