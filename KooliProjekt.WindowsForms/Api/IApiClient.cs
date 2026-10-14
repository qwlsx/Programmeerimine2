namespace KooliProjekt.WindowsForms.Api;

public interface IApiClient
{
    Task<OperationResult<PagedResult<EventModel>>> ListAsync();
    Task<OperationResult> SaveAsync(EventModel model);
    Task<OperationResult> DeleteAsync(int id);
}
