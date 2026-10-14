namespace KooliProjekt.WindowsForms.Api;

public class EventModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; } = DateTime.Now.AddDays(1);
    public string? Description { get; set; }
    public int TotalSeats { get; set; } = 1;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
