namespace ELibrary_BookService.Application.Query.Models
{
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
}
