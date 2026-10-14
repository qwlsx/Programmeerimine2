namespace KooliProjekt.Application.Data.Repositories
{
    public class EventFileRepository : BaseRepository<Event_Files>, IEventFileRepository
    {
        public EventFileRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
