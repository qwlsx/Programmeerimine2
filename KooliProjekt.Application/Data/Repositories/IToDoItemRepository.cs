using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IToDoItemRepository
    {
        Task<ToDoItem> GetByIdAsync(int id);
        Task SaveAsync(ToDoItem entity);
        Task DeleteAsync(ToDoItem entity);
    }
}