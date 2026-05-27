using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IRegistrationRepository
    {
        Task<Registration> GetByIdAsync(int id);
        Task SaveAsync(Registration entity);
        Task DeleteAsync(Registration entity);
    }
}