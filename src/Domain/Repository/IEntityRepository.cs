namespace ELibrary_BookService.Domain.Repository;

public interface IEntityRepository<T>
{
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetAsync(int id);
    Task UpdateAsync(T entity);
}
