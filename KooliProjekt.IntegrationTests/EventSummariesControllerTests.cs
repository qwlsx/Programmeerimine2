using KooliProjekt.Application.Data;

namespace KooliProjekt.IntegrationTests;

public class EventSummariesControllerTests : ControllerTestBase
{
    public EventSummariesControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("EventSummaries");
    [Fact] public Task Get_should_work() => GetShouldSucceed("EventSummaries");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("EventSummaries", new { Id = 1, EventId = 1, SummaryText = "Обновлённые итоги" });
    [Fact]
    public Task Delete_should_work() => DeleteShouldSucceed("EventSummaries", async db =>
    {
        var eventEntity = await AddEvent(db);
        var entity = new Event_Summary { EventId = eventEntity.Id, SummaryText = "Удалить" };
        db.Event_Summaries.Add(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    });
}
