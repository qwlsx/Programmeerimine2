namespace KooliProjekt.Application.Data.Repositories
{
    public class ToDoItemRepository : BaseRepository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}