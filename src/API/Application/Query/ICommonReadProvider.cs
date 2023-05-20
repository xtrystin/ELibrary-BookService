using ELibrary_BookService.Application.Query.Models;

namespace ELibrary_BookService.Application.Query
{
    public interface ICommonReadProvider
    {
        Task<AuthorReadModel> GetAuthor(int id);
        Task<List<AuthorReadModel>> GetAuthors();
        Task<List<CategoryReadModel>> GetCategories();
        Task<CategoryReadModel> GetCategory(int id);
        Task<TagReadModel> GetTag(int id);
        Task<List<TagReadModel>> GetTags();
    }
}
