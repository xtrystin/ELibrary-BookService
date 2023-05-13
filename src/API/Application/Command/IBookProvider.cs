namespace ELibrary_BookService.Application.Command
{
    public interface IBookProvider
    {
        Task DeleteBook(int id);
    }
}
