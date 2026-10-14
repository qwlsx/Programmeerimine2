using System;

namespace KooliProjekt.Application.DTOs
{
    public class EventProgramDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime Time { get; set; }
        public string Topic { get; set; } = string.Empty;
    }
}
