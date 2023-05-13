using ELibrary_BookService.Domain.Entity;

namespace ELibrary_BookService.Domain.Repository
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<bool> Exists(string name);
    }
}
