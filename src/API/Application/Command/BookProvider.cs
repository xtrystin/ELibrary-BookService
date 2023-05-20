using ELibrary_BookService.Application.Command.Exception;
using ELibrary_BookService.Application.Command.Model;
using RabbitMqMessages;
using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using ELibrary_BookService.Domain.ValueObject;
using MassTransit;

namespace ELibrary_BookService.Application.Command;

public class BookProvider : IBookProvider
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBus _bus;

    public BookProvider(IBookRepository bookRepository, IAuthorRepository authorRepository,
        ITagRepository tagRepository, ICategoryRepository categoryRepository, IBus bus)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _tagRepository = tagRepository;
        _categoryRepository = categoryRepository;
        _bus = bus;
    }

    public async Task CreateBook(CreateBookModel bookData)
    {
        var book = new Book(new Title(bookData.Title), new Description(bookData.Description),
            bookData.ImageUrl, bookData.BookAmount, bookData.PdfUrl);

        if (bookData.AuthorsId?.Count == 0)
            throw new System.Exception("AuthorIds cannot be empty");

        await AddAuthors(book, bookData.AuthorsId);
        await AddCategories(book, bookData.CategoriesId);
        await AddTags(book, bookData.TagsId);

        await _bookRepository.AddAsync(book);

        var message = new BookAvailabilityChanged() { BookId = book.Id.Value, Amount = bookData.BookAmount };
        //await _bus.Publish(message);  // todo: uncomment when rabbit is ready
    }

    private async Task AddTags(Book book, List<int>? tagsId)
    {
        foreach (var tagId in tagsId)
        {
            var tag = await _tagRepository.GetAsync(tagId);
            if (tag is null)
                throw new EntityNotFoundException("Tag has not been found");

            book.AddTag(tag);
        }
    }

    private async Task AddCategories(Book book, List<int>? categoriesId)
    {
        foreach (var catId in categoriesId)
        {
            var category = await _categoryRepository.GetAsync(catId);
            if (category is null)
                throw new EntityNotFoundException("Category has not been found");

            book.AddCategory(category);
        }
    }

    private async Task AddAuthors(Book book, List<int>? authorsId)
    {
        foreach (var authorId in authorsId)
        {
            var author = await _authorRepository.GetAsync(authorId);
            if (author is null)
                throw new EntityNotFoundException("Author has not been found");

            book.AddAuthor(author);
        }
    }

    public async Task DeleteBook(int id)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book is null)
            throw new EntityNotFoundException("Book has not been found");

        await _bookRepository.DeleteAsync(book);

        var message = new BookAvailabilityChanged() { BookId = id, Amount = -999999999 };
        //await _bus.Publish(message); // todo: uncomment when rabbit is ready
    }

    public async Task ChangeBookAmount(int id, int amount)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book is null)
            throw new EntityNotFoundException("Book has not been found");

        book.ChangeBookAmount(amount);
        await _bookRepository.UpdateAsync(book);

        var message = new BookAvailabilityChanged() { BookId = id, Amount = amount };
        // await _bus.Publish(message); // todo: uncomment when rabbit is ready
    }

    public async Task ModifyBook(int id, ModifyBookModel bookData)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book is null)
            throw new EntityNotFoundException("Book has not been found");

        book.Modify(bookData.NewTitle, bookData.NewDescription, bookData.NewImageUrl, bookData.NewPdfUrl);
        await _bookRepository.UpdateAsync(book);
    }
}