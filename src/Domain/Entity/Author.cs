using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.Entity;

public class Author
{
    public int Id { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }

    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books;
}
