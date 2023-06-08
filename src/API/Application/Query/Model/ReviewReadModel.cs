namespace ELibrary_BookService.Application.Query.Model;

public class ReviewReadModel
{
    public int ReviewId { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public ReviewReadModel(int id, string content, string userId, string firstName, string lastName, string email)
    {
        ReviewId = id;
        Content = content;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public ReviewReadModel(int id, string userId, string content)
    {
        ReviewId = id;
        UserId = userId;
        Content = content;
    }
}
