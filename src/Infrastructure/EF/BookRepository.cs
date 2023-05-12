using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.EF
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;

        public BookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Book book)
        {
            await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Book?> GetAsync(int id) 
            => _dbContext.Books.FirstOrDefault(x => x.Id == id);

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
