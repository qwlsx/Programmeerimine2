using KooliProjekt.Application.Data;

namespace KooliProjekt.IntegrationTests;

public class UsersControllerTests : ControllerTestBase
{
    public UsersControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("Users");
    [Fact] public Task Get_should_work() => GetShouldSucceed("Users");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("Users", new { Id = 1, Name = "Изменённый пользователь", Email = "user1@test.ee", Role = "User" });
    [Fact] public Task Delete_should_work() => DeleteShouldSucceed("Users", async db => (await AddUser(db)).Id);
}
