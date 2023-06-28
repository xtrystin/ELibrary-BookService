using ELibrary_BookService.Domain.Exception;
using ELibrary_BookService.Domain.ValueObject;

namespace ELibrary_BookService.Domain.Entity;

public class Book
{
    public int? Id { get; private set; }    // nullable, because DB will auto generate ID on insert
    private Title _title;
    private Description _description;
    private DateTime _createdDate;
    private string _imageUrl;
    private int _bookAmount;
    private string? _pdfUrl;

    private List<Author> _authors = new();
    private List<Category> _categories = new();
    private List<Tag> _tags = new();

    public IReadOnlyCollection<Category> Categories => _categories;
    public IReadOnlyCollection<Author> Autors => _authors;      //todo: fix typo
    public IReadOnlyCollection<Tag> Tags => _tags;


    protected Book() { }

    // Builder
    public Book(Title title, Description description, string imageUrl, int bookAmount, string? pdfUrl)
    {
        _title = title;
        _description = description;
        _createdDate = DateTime.UtcNow;
        _imageUrl = imageUrl;
        _bookAmount = bookAmount;
        _pdfUrl = pdfUrl;

    }

    protected Book(Title title, Description description, string imageUrl, int bookAmount, string? pdfUrl, List<Author> authors,
        List<Category> categories, List<Tag> tags)
    {
        _title = title;
        _description = description;
        _createdDate = DateTime.Now;
        _imageUrl = imageUrl;
        _bookAmount = bookAmount;
        _pdfUrl = pdfUrl;
        _authors = authors;
        _categories = categories;
        _tags = tags;
    }

    public void AddCategory(Category category)
    {
        if (_categories.Contains(category))
            throw new AlreadyExistsException($"Book has already this category: {category.Name}");
        
                _categories.Add(category);
    }

    public void RemoveCategory(Category category)
    {
        
        if (_categories.Contains(category) is false) 
            throw new NoItemException($"Book does not have given category: {category.Name}");

        _categories.Remove(category);
    }

    public void AddTag(Tag tag) 
    {
        if (_tags.Contains(tag))
            throw new AlreadyExistsException($"Book has already this tag: {tag.Name}");

        _tags.Add(tag);
    }

    public void RemoveTag(Tag tag)
    {
        if (_tags.Contains(tag) is false)
            throw new NoItemException($"Book does not have given tag: {tag.Name}");

        _tags.Remove(tag);
    }

    public void ChangeBookAmount(int amount)
    {
        if (_bookAmount + amount < 0)
            throw new System.Exception("Book amount cannot be less than zero");
        
         _bookAmount += amount;
    }

    public void AddAuthor(Author author)
    {
        if (_authors.Contains(author))
            throw new AlreadyExistsException($"Book has already this author: {author.Firstname} {author.Lastname}");

        _authors.Add(author);
    }

    public void RemoveAuthor(Author author)
    {
        if (_authors.Contains(author) is false)
            throw new NoItemException($"Book does not have given author: {author.Firstname} {author.Lastname}");

        _authors.Remove(author);
    }

    public void Modify(string? title, string? description, string? imageUrl, string? pdfUrl)
    {
        _title = string.IsNullOrEmpty(title) ? _title : new Title(title);
        _description = string.IsNullOrEmpty(description) ? _description : new Description(description);
        _imageUrl = string.IsNullOrEmpty(imageUrl) ? _imageUrl : imageUrl;
        _pdfUrl = pdfUrl is null ? _pdfUrl : (pdfUrl == "") ? null : pdfUrl;
    }
}
