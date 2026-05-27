#nullable enable
using System.ComponentModel.DataAnnotations; // <--- Добавил для лимитов по строкам

namespace KooliProjekt.Application.Data
{
    public class Event_Files
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "Ссылка на файл обязательна")]
        [StringLength(500, ErrorMessage = "Ссылка на файл не может быть длиннее 500 символов")]
        public string FileUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип файла обязателен")]
        [StringLength(50, ErrorMessage = "Тип файла не может быть длиннее 50 символов")]
        public string FileType { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}