using Newtonsoft.Json;
using System.Text;

namespace KooliProjekt.WindowsForms.Api;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<OperationResult<PagedResult<EventModel>>> ListAsync() =>
        SendAsync<OperationResult<PagedResult<EventModel>>>(HttpMethod.Get, "api/Events/List?page=1&pageSize=100");

    public Task<OperationResult> SaveAsync(EventModel model) =>
        SendAsync<OperationResult>(HttpMethod.Post, "api/Events/Save", model);

    public Task<OperationResult> DeleteAsync(int id) =>
        SendAsync<OperationResult>(HttpMethod.Delete, $"api/Events/Delete?id={id}");

    private async Task<T> SendAsync<T>(HttpMethod method, string url, object? body = null) where T : OperationResult, new()
    {
        try
        {
            using var request = new HttpRequestMessage(method, url);
            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            using var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var result = string.IsNullOrWhiteSpace(json) ? new T() : JsonConvert.DeserializeObject<T>(json) ?? new T();
            if (!response.IsSuccessStatusCode && !result.HasErrors)
            {
                result.Errors = new List<string> { $"API вернул ошибку {(int)response.StatusCode}" };
            }
            return result;
        }
        catch (Exception ex)
        {
            return new T { Errors = new List<string> { ex.Message } };
        }
    }
}
