using ELibrary_BookService.Domain.Entity;

namespace ELibrary_BookService.Domain.Repository
{
    public interface IAuthorRepository : IEntityRepository<Author>
    {
        Task<bool> Exists(string firstname, string lastname);
    }
}
