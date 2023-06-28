namespace ELibrary_BookService.Application.Query.Models
{
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
}
