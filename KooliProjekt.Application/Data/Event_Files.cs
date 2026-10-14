#nullable enable
using System.ComponentModel.DataAnnotations; // <--- Добавил для лимитов по строкам

namespace KooliProjekt.Application.Data
{
    public class Event_Files : Entity
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Ссылка на файл обязательна")]
        [StringLength(500, ErrorMessage = "Ссылка на файл не может быть длиннее 500 символов")]
        public string FileUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип файла обязателен")]
        [StringLength(10, ErrorMessage = "Тип файла не может быть длиннее 10 символов")]
        public string FileType { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}
