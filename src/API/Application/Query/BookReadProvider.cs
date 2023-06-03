using Dapper;
using ELibrary_BookService.Application.Query.Model;
using ELibrary_BookService.Application.Query.Models;
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

            string sql = @"SELECT ""Book"".""Id"", ""Book"".""BookAmount"", ""Book"".""CreatedDate"", ""Book"".""Description"", ""Book"".""ImageUrl"", ""Book"".""PdfUrl"", ""Book"".""Title"", ""Category"".""Id"", ""Category"".""Name"", ""Tag"".""Id"", ""Tag"".""Name"", ""Author"".""Id"", ""Author"".""Firstname"", ""Author"".""Lastname"", r.""Id"", r.""UserId"", r.""Like"", rev.""Id"", rev.""UserId"", rev.""Content""
                    FROM ""bookService"".""Book""
                    LEFT JOIN ""bookService"".""BookCategory"" ON ""BookCategory"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Category"" ON ""Category"".""Id"" = ""BookCategory"".""CategoriesId""
                    LEFT JOIN ""bookService"".""BookTag"" ON ""BookTag"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Tag"" ON ""Tag"".""Id"" = ""BookTag"".""TagsId""
                    LEFT JOIN ""bookService"".""AuthorBook"" ON ""AuthorBook"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Author"" ON ""Author"".""Id"" = ""AuthorBook"".""AutorsId""
                    left join ""userService"".""Reaction"" r on ""Book"".""Id"" = r.""BookId"" 
                    left join ""userService"".""Review"" rev on ""Book"".""Id"" = rev.""BookId"" 
                    WHERE ""Book"".""Id"" = @Id
";

            var books = await connection.QueryAsync<BookReadModel, CategoryReadModel, TagReadModel, AuthorReadModel, ReactionReadModel, ReviewReadModel,
                (BookReadModel BookReadModel, CategoryReadModel CategoryReadModel, TagReadModel TagReadModel, AuthorReadModel AuthorReadModel, ReactionReadModel ReactionReadModel, ReviewReadModel ReviewReadModel)>(sql, (book, category, tag, author, reaction, review) => (book, category, tag, author, reaction, review), param: new { Id = id }, splitOn: "Id");

            // Map multiple many to many relations to book object's lists
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

                    book.Reactions = g.Select(ub => ub.ReactionReadModel).Where(x => x != null).GroupBy(x => x.ReactionId)
                        .Select(x => new ReactionReadModel(x.First().ReactionId, x.First().UserId, x.First().Like)).ToList();

                    book.Reviews = g.Select(ub => ub.ReviewReadModel).Where(x => x != null).GroupBy(x => x.ReviewId)
                        .Select(x => new ReviewReadModel(x.First().ReviewId, x.First().UserId, x.First().Content)).ToList();

                    return book;
                });

            return result.FirstOrDefault();
        }

        public async Task<List<BookReadModel>> GetBooks()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string sql = @"SELECT ""Book"".""Id"", ""Book"".""BookAmount"", ""Book"".""CreatedDate"", ""Book"".""Description"", ""Book"".""ImageUrl"", ""Book"".""PdfUrl"", ""Book"".""Title"", ""Category"".""Id"", ""Category"".""Name"", ""Tag"".""Id"", ""Tag"".""Name"", ""Author"".""Id"", ""Author"".""Firstname"", ""Author"".""Lastname"", r.""Id"", r.""UserId"", r.""Like"", rev.""Id"", rev.""UserId"", rev.""Content""
                    FROM ""bookService"".""Book""
                    LEFT JOIN ""bookService"".""BookCategory"" ON ""BookCategory"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Category"" ON ""Category"".""Id"" = ""BookCategory"".""CategoriesId""
                    LEFT JOIN ""bookService"".""BookTag"" ON ""BookTag"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Tag"" ON ""Tag"".""Id"" = ""BookTag"".""TagsId""
                    LEFT JOIN ""bookService"".""AuthorBook"" ON ""AuthorBook"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Author"" ON ""Author"".""Id"" = ""AuthorBook"".""AutorsId""
                    left join ""userService"".""Reaction"" r on ""Book"".""Id"" = r.""BookId"" 
                    left join ""userService"".""Review"" rev on ""Book"".""Id"" = rev.""BookId"" 
";

            var books = await connection.QueryAsync<BookReadModel, CategoryReadModel, TagReadModel, AuthorReadModel, ReactionReadModel, ReviewReadModel,
                (BookReadModel BookReadModel, CategoryReadModel CategoryReadModel, TagReadModel TagReadModel, AuthorReadModel AuthorReadModel, ReactionReadModel ReactionReadModel, ReviewReadModel ReviewReadModel)>(sql,
                (book, category, tag, author, reaction, review) => (book, category, tag, author, reaction, review), splitOn: "Id");

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

                    book.Reactions = g.Select(ub => ub.ReactionReadModel).Where(x => x != null).GroupBy(x => x.ReactionId)
                        .Select(x => new ReactionReadModel(x.First().ReactionId, x.First().UserId, x.First().Like)).ToList();

                    book.Reviews = g.Select(ub => ub.ReviewReadModel).Where(x => x != null).GroupBy(x => x.ReviewId)
                        .Select(x => new ReviewReadModel(x.First().ReviewId, x.First().UserId, x.First().Content)).ToList();

                    return book;
                });

            return result.ToList();
        }

        public async Task<List<BookReadModel>?> GetBooksByFilter(int? catId, int? tagId, int? authorId)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresResourceDb"));

            string catFilter = catId != null ? @"AND ""Category"".""Id"" = @CatId" : "";
            string tagFilter = tagId != null ? @"AND ""Tag"".""Id"" = @TagId" : "";
            string authorFilter = authorId != null ? @"AND ""Author"".""Id"" = @AuthorId" : "";

            string sql = $@"SELECT ""Book"".""Id"", ""Book"".""BookAmount"", ""Book"".""CreatedDate"", ""Book"".""Description"", ""Book"".""ImageUrl"", ""Book"".""PdfUrl"", ""Book"".""Title"", ""Category"".""Id"", ""Category"".""Name"", ""Tag"".""Id"", ""Tag"".""Name"", ""Author"".""Id"", ""Author"".""Firstname"", ""Author"".""Lastname"", r.""Id"", r.""UserId"", r.""Like"", rev.""Id"", rev.""UserId"", rev.""Content""
                    FROM ""bookService"".""Book""
                    LEFT JOIN ""bookService"".""BookCategory"" ON ""BookCategory"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Category"" ON ""Category"".""Id"" = ""BookCategory"".""CategoriesId""
                    LEFT JOIN ""bookService"".""BookTag"" ON ""BookTag"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Tag"" ON ""Tag"".""Id"" = ""BookTag"".""TagsId""
                    LEFT JOIN ""bookService"".""AuthorBook"" ON ""AuthorBook"".""BooksId"" = ""Book"".""Id""
                    LEFT JOIN ""bookService"".""Author"" ON ""Author"".""Id"" = ""AuthorBook"".""AutorsId""
                    left join ""userService"".""Reaction"" r on ""Book"".""Id"" = r.""BookId"" 
                    left join ""userService"".""Review"" rev on ""Book"".""Id"" = rev.""BookId"" 
                    WHERE 1 = 1 {catFilter} {tagFilter} {authorFilter}
";

            var books = await connection.QueryAsync<BookReadModel, CategoryReadModel, TagReadModel, AuthorReadModel, ReactionReadModel, ReviewReadModel,
                (BookReadModel BookReadModel, CategoryReadModel CategoryReadModel, TagReadModel TagReadModel, AuthorReadModel AuthorReadModel, ReactionReadModel ReactionReadModel, ReviewReadModel ReviewReadModel)>(sql, (book, category, tag, author, reaction, review) => (book, category, tag, author, reaction, review), param: new { CatId = catId, TagId = tagId, AuthorId = authorId }, splitOn: "Id");

            // Map multiple many to many relations to book object's lists
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

                    book.Reactions = g.Select(ub => ub.ReactionReadModel).Where(x => x != null).GroupBy(x => x.ReactionId)
                        .Select(x => new ReactionReadModel(x.First().ReactionId, x.First().UserId, x.First().Like)).ToList();

                    book.Reviews = g.Select(ub => ub.ReviewReadModel).Where(x => x != null).GroupBy(x => x.ReviewId)
                        .Select(x => new ReviewReadModel(x.First().ReviewId, x.First().UserId, x.First().Content)).ToList();

                    return book;
                });

            return result.ToList();
        }
    }
}
