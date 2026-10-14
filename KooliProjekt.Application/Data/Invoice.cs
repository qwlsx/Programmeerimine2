#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    // Класс Invoice теперь наследует Id от Entity
    public class Invoice : Entity
    {
        public int RegistrationId { get; set; }

        [Required(ErrorMessage = "Номер счета обязателен")]
        [StringLength(20, ErrorMessage = "Номер счета не может быть длиннее 20 символов")]
        public string Number { get; set; } = string.Empty;

        [Range(0.0, double.MaxValue, ErrorMessage = "Сумма счета не может быть отрицательной")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Статус счета обязателен")]
        [StringLength(20, ErrorMessage = "Статус не может быть длиннее 20 символов")]
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public Registration? Registration { get; set; }
    }
}
