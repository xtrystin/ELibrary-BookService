using ELibrary_BookService.Domain.ValueObject;

namespace ELibrary_BookService.Application.Query.Models
{
    public class BookReadModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageUrl { get; set; }
        public int BookAmount { get; set; }
        public string? PdfUrl { get; set; }
        public List<CategoryReadModel> Categories { get; set; }
        public List<TagReadModel> Tags { get; set; }
        public List<AuthorReadModel> Authors { get; set; }

    }
}
