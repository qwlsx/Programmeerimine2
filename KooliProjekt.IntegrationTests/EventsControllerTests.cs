namespace KooliProjekt.IntegrationTests;

public class EventsControllerTests : ControllerTestBase
{
    public EventsControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("Events");
    [Fact] public Task Get_should_work() => GetShouldSucceed("Events");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("Events", new { Id = 1, Name = "Обновлённое мероприятие", StartDateTime = DateTime.Now.AddMonths(2), Description = "Описание", TotalSeats = 80, Price = 20m });
    [Fact] public Task Delete_should_work() => DeleteShouldSucceed("Events", async db => (await AddEvent(db)).Id);
}
