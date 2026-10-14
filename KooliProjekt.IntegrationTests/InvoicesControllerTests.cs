namespace KooliProjekt.IntegrationTests;

public class InvoicesControllerTests : ControllerTestBase
{
    public InvoicesControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("Invoices");
    [Fact] public Task Get_should_work() => GetShouldSucceed("Invoices");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("Invoices", new { Id = 1, RegistrationId = 1, Number = "INV-100", TotalAmount = 75m, Status = "Draft" });
    [Fact] public Task Delete_should_work() => DeleteShouldSucceed("Invoices", async db => (await AddInvoice(db)).Id);
}
