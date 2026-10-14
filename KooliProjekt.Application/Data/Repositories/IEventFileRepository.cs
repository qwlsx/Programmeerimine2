using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IEventFileRepository
    {
        Task<Event_Files> GetByIdAsync(int id);
        Task SaveAsync(Event_Files entity);
        Task DeleteAsync(Event_Files entity);
    }
}
