namespace KooliProjekt.Application.Data.Repositories
{
    public class EventProgramRepository : BaseRepository<Event_Program>, IEventProgramRepository
    {
        public EventProgramRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
