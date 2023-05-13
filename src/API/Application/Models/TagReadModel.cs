namespace ELibrary_BookService.Application.Dto
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
