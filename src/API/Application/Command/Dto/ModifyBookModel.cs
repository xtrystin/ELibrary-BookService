namespace ELibrary_BookService.Application.Command.Dto
{
    public class ModifyBookModel
    {
        public string? NewTitle { get; set; }
        public string? NewDescription { get; set; }
        public string? NewImageUrl { get; set; }
        public string? NewPdfUrl { get; set; }
    }
}
