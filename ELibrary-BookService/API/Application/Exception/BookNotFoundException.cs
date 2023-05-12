namespace ELibrary_BookService.Application.Exception
{
    public class BookNotFoundException : System.Exception
    {
        public BookNotFoundException() : base("Book has not been found")
        {
        }
    }
}
