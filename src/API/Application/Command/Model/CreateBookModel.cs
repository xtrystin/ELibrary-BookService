namespace ELibrary_BookService.Application.Command.Model
{
    public class CreateBookModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int BookAmount { get; set; }
        public string? PdfUrl { get; set; }
        public List<int>? AuthorsId { get; set; } = new();
        public List<int>? CategoriesId { get; set; } = new();
        public List<int>? TagsId { get; set; } = new();
    }
}
