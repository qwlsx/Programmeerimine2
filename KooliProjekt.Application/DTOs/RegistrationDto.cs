namespace KooliProjekt.Application.DTOs
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Status { get; set; } = string.Empty;
        public System.DateTime RegisteredAt { get; set; }
    }
}
