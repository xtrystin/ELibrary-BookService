namespace ELibrary_BookService.Application.Query
{
    public class GetBooksQuery
    {
        public string? Category { get; set; }
        public string? Tag { get; set; }
        public string? Author { get; set; }
    }
}
