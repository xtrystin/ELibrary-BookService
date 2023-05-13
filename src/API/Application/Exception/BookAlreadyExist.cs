namespace ELibrary_BookService.Application.Exception
{
    public class BookAlreadyExist : System.Exception
    {
        public BookAlreadyExist() : base("Book already exist")
        {
        }
    }
}
