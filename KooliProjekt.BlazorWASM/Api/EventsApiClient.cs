using System.Net.Http.Json;

namespace KooliProjekt.BlazorWASM.Api;

public class EventsApiClient
{
    private readonly HttpClient _httpClient;
    public EventsApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<OperationResult<PagedResult<EventModel>>> ListAsync() =>
        await _httpClient.GetFromJsonAsync<OperationResult<PagedResult<EventModel>>>("api/Events/List?page=1&pageSize=100") ?? new();

    public async Task<OperationResult<EventModel>?> GetAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/Events/Get?id={id}");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<OperationResult<EventModel>>() : null;
    }

    public async Task<OperationResult> SaveAsync(EventModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Events/Save", model);
        return await response.Content.ReadFromJsonAsync<OperationResult>() ?? new();
    }
}
