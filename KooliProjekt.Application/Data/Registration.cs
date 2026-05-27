#nullable enable
using System;
using System.ComponentModel.DataAnnotations; // <--- Подключил для лимитов по строкам

namespace KooliProjekt.Application.Data
{
    // Класс Registration теперь наследует Id от Entity
    public class Registration : Entity
    {
        public int UserId { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "Статус регистрации обязателен")]
        [StringLength(20, ErrorMessage = "Статус не может быть длиннее 20 символов")]
        public string Status { get; set; } = string.Empty;

        public DateTime RegisteredAt { get; set; }

        public User? User { get; set; }
        public Event? Event { get; set; }
    }
}