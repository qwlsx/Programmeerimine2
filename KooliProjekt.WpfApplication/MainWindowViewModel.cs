using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using KooliProjekt.WpfApplication.Api;

namespace KooliProjekt.WpfApplication;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    private readonly IApiClient _apiClient;
    private readonly IDialogProvider _dialogProvider;
    private EventModel? _selectedEvent;

    public MainWindowViewModel() : this(
        new ApiClient(new HttpClient { BaseAddress = new Uri("http://localhost:5086/") }),
        new DialogProvider())
    {
    }

    public MainWindowViewModel(IApiClient apiClient, IDialogProvider dialogProvider)
    {
        _apiClient = apiClient;
        _dialogProvider = dialogProvider;
        AddNewCommand = new RelayCommand(() => { AddNew(); return Task.CompletedTask; });
        SaveCommand = new RelayCommand(SaveAsync, () => SelectedEvent != null);
        DeleteCommand = new RelayCommand(DeleteAsync, () => SelectedEvent?.Id > 0);
    }

    public ObservableCollection<EventModel> Events { get; } = new();

    public EventModel? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            if (!SetProperty(ref _selectedEvent, value)) return;
            ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
        }
    }

    public ICommand AddNewCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public async Task LoadDataAsync()
    {
        var response = await _apiClient.ListAsync();
        if (response.HasErrors || response.Value == null)
        {
            ShowError(response);
            return;
        }
        Events.Clear();
        foreach (var item in response.Value.Results) Events.Add(item);
        SelectedEvent = Events.FirstOrDefault();
    }

    public void AddNew()
    {
        var item = new EventModel { StartDateTime = DateTime.Now.AddDays(1), TotalSeats = 1 };
        Events.Add(item);
        SelectedEvent = item;
    }

    public async Task SaveAsync()
    {
        if (SelectedEvent == null) return;
        var response = await _apiClient.SaveAsync(SelectedEvent);
        if (response.HasErrors) { ShowError(response); return; }
        await LoadDataAsync();
    }

    public async Task DeleteAsync()
    {
        if (SelectedEvent == null || SelectedEvent.Id <= 0 || !_dialogProvider.ConfirmDelete()) return;
        var response = await _apiClient.DeleteAsync(SelectedEvent.Id);
        if (response.HasErrors) { ShowError(response); return; }
        await LoadDataAsync();
    }

    private void ShowError(OperationResult response)
    {
        var message = response.ErrorMessage();
        _dialogProvider.ShowError(string.IsNullOrWhiteSpace(message) ? "Неизвестная ошибка" : message);
    }
}
