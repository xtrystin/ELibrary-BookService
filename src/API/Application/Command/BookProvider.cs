using ELibrary_BookService.Application.Command.Exception;
using ELibrary_BookService.Application.Command.Model;
using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using ELibrary_BookService.Domain.ValueObject;
using ELibrary_BookService.ServiceBus;
using MassTransit;
using ServiceBusMessages;

namespace ELibrary_BookService.Application.Command;

public class BookProvider : IBookProvider
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessagePublisher _messagePublisher;

    public BookProvider(IBookRepository bookRepository, IAuthorRepository authorRepository,
        ITagRepository tagRepository, ICategoryRepository categoryRepository, IMessagePublisher messagePublisher)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _tagRepository = tagRepository;
        _categoryRepository = categoryRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task CreateBook(CreateBookModel bookData)
    {
        var book = new Book(new Title(bookData.Title), new Description(bookData.Description),
            bookData.ImageUrl, bookData.BookAmount, bookData.PdfUrl);

        if (bookData.AuthorsId?.Count == 0)
            throw new System.Exception("AuthorIds cannot be empty");

        await _bookRepository.AddAsync(book);   // Add now to get bookId

        await AddAuthors(book.Id.Value, bookData.AuthorsId);
        await AddToCategories(book.Id.Value, bookData.CategoriesId);
        await AddToTags(book.Id.Value, bookData.TagsId);

        await _bookRepository.UpdateAsync(book);

        var message = new BookCreated() { BookId = book.Id.Value, Amount = bookData.BookAmount };
        await _messagePublisher.Publish(message);
    }

    public async Task AddToTags(int bookId, List<int>? tagsId)
    {
        var book = await GetBookOrThrow(bookId);

        foreach (var tagId in tagsId)
        {
            var tag = await _tagRepository.GetAsync(tagId);
            if (tag is null)
                throw new EntityNotFoundException($"Tag has not been found: {tagsId}");

            book.AddTag(tag);
            await _bookRepository.UpdateAsync(book);
        }
    }
    public async Task RemoveTags(int bookId, List<int>? tagsId)
    {
        var book = await GetBookOrThrow(bookId);

        foreach (var tagId in tagsId)
        {
            var tag = await _tagRepository.GetAsync(tagId);
            if (tag is null)
                throw new EntityNotFoundException($"Tag has not been found: {tagsId}");

            book.RemoveTag(tag);
            await _bookRepository.UpdateAsync(book);
        }
    }

    public async Task AddToCategories(int bookId, List<int>? categoriesId)
    {
        var book = await GetBookOrThrow(bookId);

        foreach (var catId in categoriesId)
        {
            var category = await _categoryRepository.GetAsync(catId);
            if (category is null)
                throw new EntityNotFoundException($"Category has not been found: {catId}");

            book.AddCategory(category);
            await _bookRepository.UpdateAsync(book);
        }
    }

    public async Task RemoveCategories(int bookId, List<int>? categoriesId)
    {
        var book = await GetBookOrThrow(bookId);

        foreach (var catId in categoriesId)
        {
            var category = await _categoryRepository.GetAsync(catId);
            if (category is null)
                throw new EntityNotFoundException($"Category has not been found: {catId}");

            book.RemoveCategory(category);
            await _bookRepository.UpdateAsync(book);
        }
    }

    public async Task AddAuthors(int bookId, List<int>? authorsId)
    {
        var book = await GetBookOrThrow(bookId);

        foreach (var authorId in authorsId)
        {
            var author = await _authorRepository.GetAsync(authorId);
            if (author is null)
                throw new EntityNotFoundException($"Author has not been found: {authorId}");

            book.AddAuthor(author);
            await _bookRepository.UpdateAsync(book);
        }
    }

    public async Task RemoveAuthors(int bookId, List<int>? authorsId)
    {
        var book = await GetBookOrThrow(bookId);

        foreach (var authorId in authorsId)
        {
            var author = await _authorRepository.GetAsync(authorId);
            if (author is null)
                throw new EntityNotFoundException($"Author has not been found: {authorId}");

            book.RemoveAuthor(author);
            await _bookRepository.UpdateAsync(book);
        }
    }

    public async Task DeleteBook(int id)
    {
        var book = await GetBookOrThrow(id);

        await _bookRepository.DeleteAsync(book);

        var message = new BookAvailabilityChanged() { BookId = id, Amount = -999999999 };
        await _messagePublisher.Publish(message);
    }

    public async Task ChangeBookAmount(int id, int amount)
    {
        var book = await GetBookOrThrow(id);

        book.ChangeBookAmount(amount);
        await _bookRepository.UpdateAsync(book);

        var message = new BookAvailabilityChanged() { BookId = id, Amount = amount };
         await _messagePublisher.Publish(message);
    }

    public async Task ModifyBook(int id, ModifyBookModel bookData)
    {
        var book = await GetBookOrThrow(id);

        book.Modify(bookData.NewTitle, bookData.NewDescription, bookData.NewImageUrl, bookData.NewPdfUrl);
        await _bookRepository.UpdateAsync(book);
    }

    private async Task<Book> GetBookOrThrow(int id)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book is null)
            throw new EntityNotFoundException("Book has not been found");

        return book;
    }
}