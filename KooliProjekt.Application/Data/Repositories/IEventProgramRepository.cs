using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IEventProgramRepository
    {
        Task<Event_Program> GetByIdAsync(int id);
        Task SaveAsync(Event_Program entity);
        Task DeleteAsync(Event_Program entity);
    }
}
