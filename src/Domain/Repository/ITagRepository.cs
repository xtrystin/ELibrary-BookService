using ELibrary_BookService.Domain.Entity;

namespace ELibrary_BookService.Domain.Repository
{
    public interface ITagRepository : IEntityRepository<Tag>
    {
        Task<bool> Exists(string name);
    }
}
