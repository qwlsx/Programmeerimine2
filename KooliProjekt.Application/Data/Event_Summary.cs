#nullable enable
using System.ComponentModel.DataAnnotations; // <--- Подключил для лимитов по строкам

namespace KooliProjekt.Application.Data
{
    public class Event_Summary : Entity
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Текст итогов мероприятия обязателен для заполнения")]
        public string SummaryText { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}
