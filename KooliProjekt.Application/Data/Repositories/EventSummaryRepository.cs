namespace KooliProjekt.Application.Data.Repositories
{
    public class EventSummaryRepository : BaseRepository<Event_Summary>, IEventSummaryRepository
    {
        public EventSummaryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
