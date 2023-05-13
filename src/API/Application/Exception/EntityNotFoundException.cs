namespace ELibrary_BookService.Application.Exception
{
    public class EntityNotFoundException : System.Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
