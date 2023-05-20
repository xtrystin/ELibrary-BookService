namespace ELibrary_BookService.Application.Command.Exception
{
    public class EmptyException : System.Exception
    {
        public EmptyException(string message) : base(message) { }
    }
}
