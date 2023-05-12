using ELibrary_BookService.Domain.Entity;

namespace ELibrary_BookService.Domain.Repository;

public interface IBookRepository
{
    Task<Book> GetAsync(int id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}
