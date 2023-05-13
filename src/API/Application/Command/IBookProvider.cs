using ELibrary_BookService.Application.Command.Dto;

namespace ELibrary_BookService.Application.Command
{
    public interface IBookProvider
    {
        Task CreateBook(CreateBookModel bookData);
        Task DeleteBook(int id);
    }
}
