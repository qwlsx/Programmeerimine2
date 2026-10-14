namespace KooliProjekt.Application.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentRef { get; set; }
        public string Status { get; set; } = string.Empty;
        public System.DateTime? PaidAt { get; set; }
    }
}
