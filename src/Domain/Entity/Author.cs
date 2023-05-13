using ELibrary_BookService.Domain.Exception;
using System.Xml.Linq;

namespace ELibrary_BookService.Domain.Entity;

public class Author
{
    public int? Id { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }

    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books;

    protected Author() { }
    public Author(string firstname, string lastname)
    {
        if (firstname is null || lastname is null)
            throw new NoItemException("Firstname/Lastname cannot be null");
        
        Firstname = firstname;
        Lastname = lastname;
    }
}
