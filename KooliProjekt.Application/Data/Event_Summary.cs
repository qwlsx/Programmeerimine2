#nullable enable
using System.ComponentModel.DataAnnotations; // <--- Подключил для лимитов по строкам

namespace KooliProjekt.Application.Data
{
    public class Event_Summary
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "Текст итогов мероприятия обязателен для заполнения")]
        [StringLength(2000, ErrorMessage = "Текст итогов не может превышать 2000 символов")]
        public string SummaryText { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}