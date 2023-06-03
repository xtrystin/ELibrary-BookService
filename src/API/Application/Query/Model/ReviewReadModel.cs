namespace ELibrary_BookService.Application.Query.Model;

public class ReviewReadModel
{
    public int ReviewId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }

    public ReviewReadModel(int id, string userId, string content)
    {
        ReviewId = id;
        UserId = userId;
        Content = content;
    }
}
