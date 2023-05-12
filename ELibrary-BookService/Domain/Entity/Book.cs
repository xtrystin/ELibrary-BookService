using ELibrary_BookService.Domain.Exception;
using ELibrary_BookService.Domain.ValueObject;

namespace ELibrary_BookService.Domain.Entity;

public class Book
{
    public int Id { get; private set; }
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
    public IReadOnlyCollection<Author> Autors => _authors;
    public IReadOnlyCollection<Tag> Tage => _tags;


    protected Book() { }

    protected Book(Title title, Description description, string createdDate, string imageUrl, int bookAmount, string? pdfUrl, List<Author> authors)
    {
        Random random= new Random();
        Id = random.Next();
        _title = title;
        _description = description;
        _createdDate = DateTime.Now;
        _imageUrl = imageUrl;
        _bookAmount = bookAmount;
        _pdfUrl = pdfUrl;
        _authors = authors;
    }

    public void AddCategory(Category category)
    {
        if (_categories.Contains(category))
            throw new AlreadyExistsException("Book has already this category");
        
                _categories.Add(category);
    }

    public void RemoveCategory(string categoryName)
    {
        var category = _categories.FirstOrDefault(x => x.Name == categoryName);
        if (category is null) 
        {
            throw new NoItemException("Book does not have given category");
        }

        _categories.Remove(category);
    }

    public void AddTag(Tag tag) 
    {
        if (_tags.Contains(tag))
            throw new AlreadyExistsException("Book has already this tag");

        _tags.Add(tag);
    }

    public void RemoveTag(string tagName)
    {
        var tag = _tags.FirstOrDefault(x => x.Name == tagName);
        if (_tags.Contains(tag))
            throw new NoItemException("Book does not have given tag");

        _tags.Remove(tag);
    }

    public void AddBooks(int amount) => _bookAmount += amount;

    public void RemoveBooks(int amount)
    {
        if (_bookAmount - amount < 0)
            throw new System.Exception("Book amount cannot be less than zero");
        
         _bookAmount -= amount;
    }

    public void ChangeDescription(Description description) => _description = description;

    public void AddPdfLink(string pdfUrl) => _pdfUrl = pdfUrl;

    public void ChangeImage(string imageUrl) => _imageUrl = imageUrl;
}
