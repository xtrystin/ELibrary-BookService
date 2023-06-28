using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using ELibrary_BookService.Infrastructure.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace ELibrary_BookService.Infrastructure.EF
{
    public class BookRepository : EntityRepository<Book>, IBookRepository
    {
        public BookRepository(BookDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Book?> GetAsync(int id) 
            => await _dbContext.Books.Include(book => book.Tags).Include(book => book.Categories).Include(book => book.Autors).FirstOrDefaultAsync(x => x.Id == id);
    }
}
