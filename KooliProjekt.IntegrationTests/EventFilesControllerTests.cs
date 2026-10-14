using KooliProjekt.Application.Data;

namespace KooliProjekt.IntegrationTests;

public class EventFilesControllerTests : ControllerTestBase
{
    public EventFilesControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("EventFiles");
    [Fact] public Task Get_should_work() => GetShouldSucceed("EventFiles");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("EventFiles", new { Id = 1, EventId = 1, FileUrl = "https://example.test/updated.pdf", FileType = "pdf" });
    [Fact]
    public Task Delete_should_work() => DeleteShouldSucceed("EventFiles", async db =>
    {
        var eventEntity = await AddEvent(db);
        var entity = new Event_Files { EventId = eventEntity.Id, FileUrl = "https://example.test/delete.pdf", FileType = "pdf" };
        db.Event_Files.Add(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    });
}
