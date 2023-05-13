using ELibrary_BookService.Application.Command.Dto;

namespace ELibrary_BookService.Application.Command
{
    public interface ICommonProvider
    {
        Task CreateAuthor(CreateAuthorModel authorData);
        Task CreateCategory(string name);
        Task CreateTag(string name);
        Task DeleteAuthor(int catId);
        Task DeleteCategory(int catId);
        Task DeleteTag(int tagId);
    }
}
