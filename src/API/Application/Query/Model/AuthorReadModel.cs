namespace ELibrary_BookService.Application.Query.Models
{
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
