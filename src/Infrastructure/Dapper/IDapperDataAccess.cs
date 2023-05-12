namespace ELibrary_BookService.Domain.Dapper;

public interface IDapperDataAccess
{
    public Task<IEnumerable<T>> QueryAsync<T, U>(string sql, U parameters);
}
