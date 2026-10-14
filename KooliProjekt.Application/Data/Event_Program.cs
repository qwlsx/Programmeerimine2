#nullable enable
using System;
using System.ComponentModel.DataAnnotations; // <--- Подключил для валидации

namespace KooliProjekt.Application.Data
{
    public class Event_Program : Entity
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Время проведения обязательно")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Тема/описание пункта программы обязательны")]
        [StringLength(255, ErrorMessage = "Описание пункта программы не может быть длиннее 255 символов")]
        public string Topic { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}
