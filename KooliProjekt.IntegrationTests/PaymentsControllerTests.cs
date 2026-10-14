using KooliProjekt.Application.Data;

namespace KooliProjekt.IntegrationTests;

public class PaymentsControllerTests : ControllerTestBase
{
    public PaymentsControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact] public Task List_should_work() => ListShouldSucceed("Payments");
    [Fact] public Task Get_should_work() => GetShouldSucceed("Payments");
    [Fact] public Task Save_should_work() => SaveShouldSucceed("Payments", new { Id = 1, InvoiceId = 1, Amount = 50m, PaymentMethod = "Bank", PaymentRef = "UPDATED-REF", Status = "Completed", PaidAt = DateTime.UtcNow });
    [Fact]
    public Task Delete_should_work() => DeleteShouldSucceed("Payments", async db =>
    {
        var invoice = await AddInvoice(db);
        var entity = new Payment { InvoiceId = invoice.Id, Amount = 25, PaymentMethod = "Bank", Status = "Processing" };
        db.Payments.Add(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    });
}
