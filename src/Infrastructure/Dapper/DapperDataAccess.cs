using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections;

namespace ELibrary_BookService.Infrastructure.Dapper
{
    public class DapperDataAccess : IDapperDataAccess
    {
        private readonly IConfiguration _configuration;

        public DapperDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> QueryAsync<T, U>(string sql, U parameters)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            var result = await connection.QueryAsync<T>(sql, parameters);
            return result;
        }
    }
}
