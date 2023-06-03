namespace ELibrary_BookService.Application.Query.Model;

public class ReactionReadModel
{
    public int ReactionId { get; set; }
    public string UserId { get; set; }
    public bool Like { get; set; }

    public ReactionReadModel(int id, string userId, bool like)
    {
        ReactionId = id;
        UserId= userId;
        Like = like;
    }
}
