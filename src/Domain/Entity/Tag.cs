using ELibrary_BookService.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.Entity;

public class Tag
{
    public int? Id { get; private set; }
    public string Name { get; private set; }

    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books;

    protected Tag() { }
    public Tag(string name)
    {
        Name = name;
    }

    public void ChangeName(string name)
    {
        if (name is null)
            throw new NoItemException("Tag name cannot be null");
        Name = name;
    }
}
