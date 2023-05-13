using Dapper;
using ELibrary_BookService.Application.Dto;
using ELibrary_BookService.Domain.Dapper;
using Npgsql;

namespace ELibrary_BookService.Application.Query
{
    public class BookReadProvider : IBookReadProvider
    {
        private readonly IConfiguration _configuration;

        public BookReadProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BookReadModel> GetBook(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Book"".""Id"", ""Book"".""BookAmount"", ""Book"".""CreatedDate"", ""Book"".""Description"", ""Book"".""ImageUrl"", ""Book"".""PdfUrl"", ""Book"".""Title"", ""Category"".""Id"", ""Category"".""Name"", ""Tag"".""Id"", ""Tag"".""Name"", ""Author"".""Id"", ""Author"".""Firstname"", ""Author"".""Lastname""
                    FROM ""bookService"".""Book""
                    LEFT JOIN ""bookService"".""BookCategory"" ON ""BookCategory"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Category"" ON ""Category"".""Id"" = ""BookCategory"".""CategoriesId""
                    LEFT JOIN ""bookService"".""BookTag"" ON ""BookTag"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Tag"" ON ""Tag"".""Id"" = ""BookTag"".""TagsId""
                    LEFT JOIN ""bookService"".""AuthorBook"" ON ""AuthorBook"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Author"" ON ""Author"".""Id"" = ""AuthorBook"".""AutorsId""
                    WHERE ""Book"".""Id"" = @Id
";

            var books = await connection.QueryAsync<BookReadModel, CategoryReadModel, TagReadModel, AuthorReadModel,
                (BookReadModel BookReadModel, CategoryReadModel CategoryReadModel, TagReadModel TagReadModel, AuthorReadModel AuthorReadModel)>(sql, (book, category, tag, author) => (book, category, tag, author), param: new { Id = id }, splitOn: "Id");

            var result = books.GroupBy(bc => bc.BookReadModel.Id)
                .Select(g =>
                {
                    var book = g.First().BookReadModel;

                    book.Categories = g.Select(bc => bc.CategoryReadModel).Where(x => x != null).GroupBy(x => x.Id)
                        .Select(x => new CategoryReadModel(x.First().Id, x.First().Name)).ToList();

                    book.Tags = g.Select(bc => bc.TagReadModel).Where(x => x != null).GroupBy(x => x.Id)
                        .Select(x => new TagReadModel(x.First().Id, x.First().Name)).ToList();

                    book.Authors = g.Select(bc => bc.AuthorReadModel).Where(x => x != null).GroupBy(x => x.Id)
                        .Select(x => new AuthorReadModel(x.First().Id, x.First().Firstname, x.First().Lastname)).ToList();

                    return book;
                });

            return result.FirstOrDefault();
        }

        public async Task<List<BookReadModel>> GetBooks()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Book"".""Id"", ""Book"".""BookAmount"", ""Book"".""CreatedDate"", ""Book"".""Description"", ""Book"".""ImageUrl"", ""Book"".""PdfUrl"", ""Book"".""Title"", ""Category"".""Id"", ""Category"".""Name"", ""Tag"".""Id"", ""Tag"".""Name"", ""Author"".""Id"", ""Author"".""Firstname"", ""Author"".""Lastname""
                    FROM ""bookService"".""Book""
                    LEFT JOIN ""bookService"".""BookCategory"" ON ""BookCategory"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Category"" ON ""Category"".""Id"" = ""BookCategory"".""CategoriesId""
                    LEFT JOIN ""bookService"".""BookTag"" ON ""BookTag"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Tag"" ON ""Tag"".""Id"" = ""BookTag"".""TagsId""
                    LEFT JOIN ""bookService"".""AuthorBook"" ON ""AuthorBook"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Author"" ON ""Author"".""Id"" = ""AuthorBook"".""AutorsId""
";

            var books = await connection.QueryAsync<BookReadModel, CategoryReadModel, TagReadModel, AuthorReadModel,
                (BookReadModel BookReadModel, CategoryReadModel CategoryReadModel, TagReadModel TagReadModel, AuthorReadModel AuthorReadModel)>(sql,
                (book, category, tag, author) => (book, category, tag, author), splitOn: "Id");

            var result = books.GroupBy(bc => bc.BookReadModel.Id)
                .Select(g =>
                {
                    var book = g.First().BookReadModel;

                    //var a = g.Select(bc => bc.CategoryReadModel).ToList();
                    //book.Tags = g.Select(bc => bc.TagReadModel).ToList();
                    book.Categories = g.Select(bc => bc.CategoryReadModel).Where(x => x != null).GroupBy(x => x.Id)
                        .Select(x => new CategoryReadModel(x.First().Id, x.First().Name)).ToList();

                    book.Tags = g.Select(bc => bc.TagReadModel).Where(x => x != null).GroupBy(x => x.Id)
                        .Select(x => new TagReadModel(x.First().Id, x.First().Name)).ToList();

                    book.Authors = g.Select(bc => bc.AuthorReadModel).Where(x => x != null).GroupBy(x => x.Id)
                        .Select(x => new AuthorReadModel(x.First().Id, x.First().Firstname, x.First().Lastname)).ToList();

                    return book;
                });

            return result.ToList();
        }
    }
}
