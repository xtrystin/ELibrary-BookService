using Dapper;
using ELibrary_BookService.Application.Dto;
using Npgsql;

namespace ELibrary_BookService.Application.Query
{
    public class CommonReadProvider : ICommonReadProvider
    {
        private readonly IConfiguration _configuration;

        public CommonReadProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TagReadModel> GetTag(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Id"", ""Name"" FROM ""bookService"".""Tag"" WHERE ""Id"" = @Id;";

            var result = await connection.QueryAsync<TagReadModel>(sql, param: new { Id = id });
                        
            return result?.FirstOrDefault();
        }

        public async Task<List<TagReadModel>> GetTags()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Id"", ""Name"" FROM ""bookService"".""Tag"";";

            var result = await connection.QueryAsync<TagReadModel>(sql);

            return result.ToList();
        }

        public async Task<CategoryReadModel> GetCategory(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Id"", ""Name"" FROM ""bookService"".""Category"" WHERE ""Id"" = @Id;";

            var result = await connection.QueryAsync<CategoryReadModel>(sql, param: new { Id = id });

            return result?.FirstOrDefault();
        }

        public async Task<List<CategoryReadModel>> GetCategories()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Id"", ""Name"" FROM ""bookService"".""Category"";";

            var result = await connection.QueryAsync<CategoryReadModel>(sql);

            return result.ToList();
        }

        public async Task<AuthorReadModel> GetAuthor(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Id"", ""Firstname"", ""Lastname"" FROM ""bookService"".""Author"" WHERE ""Id"" = @Id;";

            var result = await connection.QueryAsync<AuthorReadModel>(sql, param: new { Id = id });

            return result?.FirstOrDefault();
        }

        public async Task<List<AuthorReadModel>> GetAuthors()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Id"", ""Firstname"", ""Lastname"" FROM ""bookService"".""Author"";";

            var result = await connection.QueryAsync<AuthorReadModel>(sql);

            return result.ToList();
        }
    }
}
