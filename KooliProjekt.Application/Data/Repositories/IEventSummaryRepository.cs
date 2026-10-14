using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IEventSummaryRepository
    {
        Task<Event_Summary> GetByIdAsync(int id);
        Task SaveAsync(Event_Summary entity);
        Task DeleteAsync(Event_Summary entity);
    }
}
