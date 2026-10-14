namespace KooliProjekt.Application.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public string Number { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public System.DateTime CreatedAt { get; set; }
    }
}
