namespace KooliProjekt.Application.Data.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        // Если для Event нужны специфические Include (например, для файлов или регистрации),
        // добавим их сюда позже методом override GetByIdAsync.
    }
}