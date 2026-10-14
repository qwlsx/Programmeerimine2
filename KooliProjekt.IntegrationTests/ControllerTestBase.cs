using System.Net;
using System.Net.Http.Json;
using KooliProjekt.Application.Data;
using Microsoft.Extensions.DependencyInjection;

namespace KooliProjekt.IntegrationTests;

public abstract class ControllerTestBase : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    protected HttpClient Client { get; }

    protected ControllerTestBase(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        Client = factory.CreateClient();
    }

    protected async Task ListShouldSucceed(string controller)
    {
        var response = await Client.GetAsync($"/api/{controller}/List?Page=1&PageSize=10");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    protected async Task GetShouldSucceed(string controller)
    {
        var response = await Client.GetAsync($"/api/{controller}/Get?id=1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    protected async Task SaveShouldSucceed(string controller, object command)
    {
        var response = await Client.PostAsJsonAsync($"/api/{controller}/Save", command);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    protected async Task DeleteShouldSucceed(string controller, Func<ApplicationDbContext, Task<int>> addEntity)
    {
        int id;
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            id = await addEntity(dbContext);
        }

        var response = await Client.DeleteAsync($"/api/{controller}/Delete?id={id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    protected static async Task<Event> AddEvent(ApplicationDbContext dbContext)
    {
        var entity = new Event
        {
            Name = $"Удаляемое мероприятие {Guid.NewGuid():N}",
            StartDateTime = DateTime.Now.AddMonths(1),
            Description = "Интеграционный тест",
            TotalSeats = 20,
            Price = 12,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Events.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    protected static async Task<User> AddUser(ApplicationDbContext dbContext)
    {
        var entity = new User
        {
            Name = "Удаляемый пользователь",
            Email = $"delete-{Guid.NewGuid():N}@test.ee",
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Users.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    protected static async Task<Registration> AddRegistration(ApplicationDbContext dbContext)
    {
        var user = await AddUser(dbContext);
        var eventEntity = await AddEvent(dbContext);
        var entity = new Registration { UserId = user.Id, EventId = eventEntity.Id, Status = "Draft", RegisteredAt = DateTime.UtcNow };
        dbContext.Registrations.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    protected static async Task<Invoice> AddInvoice(ApplicationDbContext dbContext)
    {
        var registration = await AddRegistration(dbContext);
        var entity = new Invoice
        {
            RegistrationId = registration.Id,
            Number = $"DEL-{Guid.NewGuid():N}"[..20],
            TotalAmount = 25,
            Status = "Draft",
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Invoices.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}
