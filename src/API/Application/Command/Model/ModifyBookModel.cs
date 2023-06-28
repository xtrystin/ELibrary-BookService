namespace ELibrary_BookService.Application.Command.Model
{
    public class ModifyBookModel
    {
        public string? NewTitle { get; set; }
        public string? NewDescription { get; set; }
        public string? NewImageUrl { get; set; }
        public string? NewPdfUrl { get; set; }
    }
}
