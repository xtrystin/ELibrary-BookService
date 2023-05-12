namespace ELibrary_BookService.Application.Command
{
    public class ModifyBookCommand
    {
        public int Id { get; set; }
        public string? NewTitle { get; set; }
        public string? NewDescription { get; set; }
        public string? NewImageUrl { get; set; }
        public string? NewPdfUrl { get; set; }
    }
}
