using ELibrary_BookService.Application.Command.Model;

namespace ELibrary_BookService.Application.Command
{
    public interface IBookProvider
    {
        Task ChangeBookAmount(int id, int amount);
        Task CreateBook(CreateBookModel bookData);
        Task DeleteBook(int id);
        Task ModifyBook(int id, ModifyBookModel bookData);
    }
}
