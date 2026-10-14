using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms;

public interface IMainView
{
    IList<EventModel> DataSource { set; }
    int CurrentId { get; set; }
    string CurrentName { get; set; }
    DateTime CurrentStartDateTime { get; set; }
    string? CurrentDescription { get; set; }
    int CurrentTotalSeats { get; set; }
    decimal CurrentPrice { get; set; }
    void ShowError(string message);
    bool ConfirmDelete();
}
