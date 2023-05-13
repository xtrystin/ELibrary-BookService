namespace ELibrary_BookService.Application.Exception
{
    public class AlreadyExistsException : System.Exception
    {
        public AlreadyExistsException(string message) : base(message) { }
    }
}
