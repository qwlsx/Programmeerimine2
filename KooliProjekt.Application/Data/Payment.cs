#nullable enable
using System;
using System.ComponentModel.DataAnnotations; // <--- Подключил для лимитов

namespace KooliProjekt.Application.Data
{
    // Класс Payment теперь наследует Id от Entity
    public class Payment : Entity
    {
        public int InvoiceId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма платежа должна быть больше нуля")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Метод оплаты обязателен")]
        [StringLength(50, ErrorMessage = "Метод оплаты не может быть длиннее 50 символов")]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Номер транзакции не может быть длиннее 100 символов")]
        public string? PaymentRef { get; set; }

        [Required(ErrorMessage = "Статус платежа обязателен")]
        [StringLength(20, ErrorMessage = "Статус не может быть длиннее 20 символов")]
        public string Status { get; set; } = string.Empty;

        public DateTime? PaidAt { get; set; }

        public Invoice? Invoice { get; set; }
    }
}