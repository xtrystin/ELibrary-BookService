namespace ELibrary_BookService.Application.Command.Exception
{
    public class BookAlreadyExist : System.Exception
    {
        public BookAlreadyExist() : base("Book already exist")
        {
        }
    }
}
