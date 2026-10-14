namespace KooliProjekt.WpfApplication.Api;

public class EventModel : NotifyPropertyChangedBase
{
    private int _id;
    private string _name = string.Empty;
    private DateTime _startDateTime = DateTime.Now.AddDays(1);
    private string? _description;
    private int _totalSeats = 1;
    private decimal _price;
    private DateTime _createdAt;

    public int Id { get => _id; set => SetProperty(ref _id, value); }
    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public DateTime StartDateTime { get => _startDateTime; set => SetProperty(ref _startDateTime, value); }
    public string? Description { get => _description; set => SetProperty(ref _description, value); }
    public int TotalSeats { get => _totalSeats; set => SetProperty(ref _totalSeats, value); }
    public decimal Price { get => _price; set => SetProperty(ref _price, value); }
    public DateTime CreatedAt { get => _createdAt; set => SetProperty(ref _createdAt, value); }
}
