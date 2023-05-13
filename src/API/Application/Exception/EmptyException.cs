namespace ELibrary_BookService.Application.Exception
{
    public class EmptyException : System.Exception
    {
        public EmptyException(string message) : base(message) { }
    }
}
