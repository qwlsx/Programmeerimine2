using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms;

public class MainViewPresenter
{
    private readonly IMainView _view;
    private readonly IApiClient _apiClient;

    public MainViewPresenter(IMainView view, IApiClient apiClient)
    {
        _view = view;
        _apiClient = apiClient;
    }

    public async Task LoadData()
    {
        var response = await _apiClient.ListAsync();
        if (response.HasErrors || response.Value == null)
        {
            _view.ShowError(response.ErrorMessage());
            return;
        }
        _view.DataSource = response.Value.Results;
    }

    public void SetSelection(EventModel? model)
    {
        _view.CurrentId = model?.Id ?? 0;
        _view.CurrentName = model?.Name ?? string.Empty;
        _view.CurrentStartDateTime = model?.StartDateTime ?? DateTime.Now.AddDays(1);
        _view.CurrentDescription = model?.Description;
        _view.CurrentTotalSeats = model?.TotalSeats ?? 1;
        _view.CurrentPrice = model?.Price ?? 0;
    }

    public void AddNew() => SetSelection(null);

    public async Task Save()
    {
        var model = new EventModel
        {
            Id = _view.CurrentId,
            Name = _view.CurrentName,
            StartDateTime = _view.CurrentStartDateTime,
            Description = _view.CurrentDescription,
            TotalSeats = _view.CurrentTotalSeats,
            Price = _view.CurrentPrice
        };
        var response = await _apiClient.SaveAsync(model);
        if (response.HasErrors)
        {
            _view.ShowError(response.ErrorMessage());
            return;
        }
        await LoadData();
    }

    public async Task Delete()
    {
        if (_view.CurrentId <= 0 || !_view.ConfirmDelete()) return;
        var response = await _apiClient.DeleteAsync(_view.CurrentId);
        if (response.HasErrors)
        {
            _view.ShowError(response.ErrorMessage());
            return;
        }
        AddNew();
        await LoadData();
    }
}
