using ELibrary_BookService.Application.Command.Model;
using ELibrary_BookService.Domain.Entity;

namespace ELibrary_BookService.Application.Command
{
    public interface IBookProvider
    {
        Task AddAuthors(int bookId, List<int>? authorsId);
        Task AddToCategories(int bookId, List<int>? categoriesId);
        Task AddToTags(int bookId, List<int>? tagsId);
        Task ChangeBookAmount(int id, int amount);
        Task CreateBook(CreateBookModel bookData);
        Task DeleteBook(int id);
        Task ModifyBook(int id, ModifyBookModel bookData);
        Task RemoveAuthors(int bookId, List<int>? authorsId);
        Task RemoveCategories(int bookId, List<int>? categoriesId);
        Task RemoveTags(int bookId, List<int>? tagsId);
    }
}
