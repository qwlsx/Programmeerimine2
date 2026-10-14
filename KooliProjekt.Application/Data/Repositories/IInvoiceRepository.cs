using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(int id);
        Task<string> GetNextNumberAsync();
        Task SaveAsync(Invoice entity);
        Task DeleteAsync(Invoice entity);
    }
}
