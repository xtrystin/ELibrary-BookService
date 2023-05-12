namespace ELibrary_BookService.Application
{
    public interface IBookProvider
    {
        Task DeleteBook(int id);
    }
}
