using KooliProjekt.WpfApplication.Api;

namespace KooliProjekt.WpfApplication;

// Unit tests link the platform-independent ViewModel sources directly so that
// Windows application-control policies do not need to load the WPF executable.
internal sealed class ApiClient : IApiClient
{
    public ApiClient(HttpClient httpClient) { }

    public Task<OperationResult<PagedResult<EventModel>>> ListAsync() => throw new NotSupportedException();
    public Task<OperationResult> SaveAsync(EventModel model) => throw new NotSupportedException();
    public Task<OperationResult> DeleteAsync(int id) => throw new NotSupportedException();
}

internal sealed class DialogProvider : IDialogProvider
{
    public void ShowError(string message) { }
    public bool ConfirmDelete() => false;
}
