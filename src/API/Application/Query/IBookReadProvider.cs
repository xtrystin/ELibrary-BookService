using ELibrary_BookService.Application.Dto;

namespace ELibrary_BookService.Application.Query
{
    public interface IBookReadProvider
    {
        Task<BookReadModel> GetBook(int id);
        Task<List<BookReadModel>> GetBooks();
    }
}
