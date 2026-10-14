namespace KooliProjekt.Application.DTOs
{
    public class EventFileDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
