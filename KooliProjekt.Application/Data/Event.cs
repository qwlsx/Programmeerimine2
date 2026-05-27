#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    // Класс Event теперь наследует Id из базового класса Entity
    public class Event : Entity
    {
        [Required(ErrorMessage = "Название мероприятия обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Название не может быть длиннее 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата и время начала обязательны")]
        public DateTime StartDateTime { get; set; }

        [StringLength(500, ErrorMessage = "Описание не может быть длиннее 500 символов")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Количество мест должно быть больше 0")]
        public int TotalSeats { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}