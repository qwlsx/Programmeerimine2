namespace KooliProjekt.IntegrationTests;

public class RegistrationsControllerTests : ControllerTestBase
{
    public RegistrationsControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("Registrations");
    [Fact] public Task Get_should_work() => GetShouldSucceed("Registrations");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("Registrations", new { Id = 1, UserId = 1, EventId = 1, Status = "Draft", RegisteredAt = DateTime.UtcNow });
    [Fact] public Task Delete_should_work() => DeleteShouldSucceed("Registrations", async db => (await AddRegistration(db)).Id);
}
