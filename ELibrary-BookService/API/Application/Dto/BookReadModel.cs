using ELibrary_BookService.Domain.ValueObject;

namespace ELibrary_BookService.Application.Dto
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

    public class CategoryReadModel
    {
        public CategoryReadModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class TagReadModel
    {
        public TagReadModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class AuthorReadModel
    {
        public AuthorReadModel(int id, string firstname, string lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
}
