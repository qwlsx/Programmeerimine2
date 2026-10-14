using KooliProjekt.Application.Data;

namespace KooliProjekt.IntegrationTests;

public class EventProgramsControllerTests : ControllerTestBase
{
    public EventProgramsControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("EventPrograms");
    [Fact] public Task Get_should_work() => GetShouldSucceed("EventPrograms");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("EventPrograms", new { Id = 1, EventId = 1, Time = DateTime.Now.AddMonths(3), Topic = "Обновлённая программа" });
    [Fact]
    public Task Delete_should_work() => DeleteShouldSucceed("EventPrograms", async db =>
    {
        var eventEntity = await AddEvent(db);
        var entity = new Event_Program { EventId = eventEntity.Id, Time = eventEntity.StartDateTime.AddHours(1), Topic = "Удалить" };
        db.Event_Programs.Add(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    });
}
