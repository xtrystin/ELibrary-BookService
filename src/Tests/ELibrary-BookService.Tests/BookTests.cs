using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Exception;
using ELibrary_BookService.Domain.ValueObject;
using System.Reflection;

namespace ELibrary_BookService.Tests;

public class BookTests
{
    [Test]
    public void AddTag_ShouldAddTagToList()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var tag = new Tag("Tag Name");

        // Act
        book.AddTag(tag);

        // Assert
        Assert.Contains(tag, book.Tags.ToList());
    }

    [Test]
    public void AddTag_ShouldThrowAlreadyExistsException_WhenTagAlreadyExists()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var tag = new Tag("Tag Name");

        book.AddTag(tag);

        // Act and Assert
        Assert.Throws<AlreadyExistsException>(() => book.AddTag(tag));
    }

    [Test]
    public void RemoveTag_ShouldRemoveTagFromList()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var tag = new Tag("Tag Name");

        book.AddTag(tag);

        // Act
        book.RemoveTag(tag);

        // Assert
        Assert.IsFalse(book.Tags.Contains(tag));
    }

    [Test]
    public void RemoveTag_ShouldThrowNoItemException_WhenTagDoesNotExist()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var tag = new Tag("Tag Name");

        // Act and Assert
        Assert.Throws<NoItemException>(() => book.RemoveTag(tag));
    }

    [Test]
    public void ChangeBookAmount_ShouldIncreaseBookAmount()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        // Act
        book.ChangeBookAmount(5);

        // Assert
        Assert.AreEqual(15, GetPrivateFieldValue<int>(book, "_bookAmount"));
    }

    [Test]
    public void ChangeBookAmount_ShouldDecreaseBookAmount()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        // Act
        book.ChangeBookAmount(-3);

        // Assert
        Assert.AreEqual(7, GetPrivateFieldValue<int>(book, "_bookAmount"));
    }

    [Test]
    public void ChangeBookAmount_ShouldThrowException_WhenResultingAmountIsNegative()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        // Act and Assert
        Assert.Throws<Exception>(() => book.ChangeBookAmount(-15));
    }

    [Test]
    public void AddAuthor_ShouldAddAuthorToList()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var author = new Author("John", "Doe");

        // Act
        book.AddAuthor(author);

        // Assert
        Assert.Contains(author, book.Autors.ToList());
    }

    [Test]
    public void AddAuthor_ShouldThrowAlreadyExistsException_WhenAuthorAlreadyExists()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var author = new Author("John", "Doe");

        book.AddAuthor(author);

        // Act and Assert
        Assert.Throws<AlreadyExistsException>(() => book.AddAuthor(author));
    }

    [Test]
    public void RemoveAuthor_ShouldRemoveAuthorFromList()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var author = new Author("John", "Doe");

        book.AddAuthor(author);

        // Act
        book.RemoveAuthor(author);

        // Assert
        Assert.IsFalse(book.Autors.Contains(author));
    }

    [Test]
    public void RemoveAuthor_ShouldThrowNoItemException_WhenAuthorDoesNotExist()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        var author = new Author("John", "Doe");

        // Act and Assert
        Assert.Throws<NoItemException>(() => book.RemoveAuthor(author));
    }

    [Test]
    public void Modify_ShouldModifyBookProperties()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        // Act
        book.Modify("New Title", "New Description", "new-image-url", "new-pdf-url");

        // Assert
        Assert.AreEqual("New Title", GetPrivateFieldValue<Title>(book, "_title").Value);
        Assert.AreEqual("New Description", GetPrivateFieldValue<Description>(book, "_description").Value);
        Assert.AreEqual("new-image-url", GetPrivateFieldValue<string>(book, "_imageUrl"));
        Assert.AreEqual("new-pdf-url", GetPrivateFieldValue<string?>(book, "_pdfUrl"));
    }

    [Test]
    public void Modify_ShouldKeepOriginalBookProperties_WhenNewPropertiesAreNull()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        // Act
        book.Modify(null, null, null, null);

        // Assert
        Assert.AreEqual("Book Title", GetPrivateFieldValue<Title>(book, "_title").Value);
        Assert.AreEqual("Book Description", GetPrivateFieldValue<Description>(book, "_description").Value);
        Assert.AreEqual("image-url", GetPrivateFieldValue<string>(book, "_imageUrl"));
        Assert.AreEqual("pdf-url", GetPrivateFieldValue<string?>(book, "_pdfUrl"));
    }

    [Test]
    public void Modify_ShouldSetPdfUrlToNull_WhenNewPdfUrlIsEmptyString()
    {
        // Arrange
        var book = new Book(
            new Title("Book Title"),
            new Description("Book Description"),
            "image-url",
            10,
            "pdf-url"
        );

        // Act
        book.Modify(null, null, null, "");

        // Assert
        Assert.AreEqual(null, GetPrivateFieldValue<string?>(book, "_pdfUrl"));
    }


    // Helper method to access private fields using reflection
    private T GetPrivateFieldValue<T>(object obj, string fieldName)
    {
        var fieldInfo = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        return (T)fieldInfo.GetValue(obj);
    }
}