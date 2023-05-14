using ELibrary_BookService.Application.Dto;

namespace ELibrary_BookService.Application.Query
{
    public interface IBookReadProvider
    {
        Task<BookReadModel> GetBook(int id);
        Task<List<BookReadModel>> GetBooks();
        Task<List<BookReadModel>?> GetBooksByFilter(int? catId, int? tagId, int? authorId);
    }
}
