namespace KooliProjekt.Application.DTOs
{
    public class EventSummaryDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string SummaryText { get; set; } = string.Empty;
    }
}
