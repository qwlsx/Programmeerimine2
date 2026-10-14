using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.EventFiles;
using KooliProjekt.Application.Features.EventPrograms;
using KooliProjekt.Application.Features.Events;
using KooliProjekt.Application.Features.EventSummaries;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.Features.Payments;
using KooliProjekt.Application.Features.Registrations;
using KooliProjekt.Application.Features.Users;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.UnitTests.Features;

public class ListHandlersSuccessTests
{
    [Fact]
    public async Task All_list_handlers_should_return_searched_data()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        await using var db = new ApplicationDbContext(options);
        await db.Database.EnsureCreatedAsync();

        var user = new User { Name = "Ruslan", Email = "ruslan@test.ee", Role = "Admin", CreatedAt = DateTime.UtcNow };
        var eventEntity = new Event { Name = "Конференция", Description = "Технологии", StartDateTime = DateTime.UtcNow.AddDays(2), TotalSeats = 40, Price = 20, CreatedAt = DateTime.UtcNow };
        db.AddRange(user, eventEntity);
        await db.SaveChangesAsync();
        var registration = new Registration { UserId = user.Id, EventId = eventEntity.Id, Status = "Draft", RegisteredAt = DateTime.UtcNow };
        db.AddRange(
            registration,
            new Event_Program { EventId = eventEntity.Id, Time = eventEntity.StartDateTime, Topic = "Открытие" },
            new Event_Summary { EventId = eventEntity.Id, SummaryText = "Отличные итоги" },
            new Event_Files { EventId = eventEntity.Id, FileUrl = "https://example.test/program.pdf", FileType = "pdf" });
        await db.SaveChangesAsync();
        var invoice = new Invoice { RegistrationId = registration.Id, Number = "INV-TEST", TotalAmount = 20, Status = "Draft", CreatedAt = DateTime.UtcNow };
        db.Invoices.Add(invoice);
        await db.SaveChangesAsync();
        db.Payments.Add(new Payment { InvoiceId = invoice.Id, Amount = 20, PaymentMethod = "Bank", PaymentRef = "REF-TEST", Status = "Processing" });
        await db.SaveChangesAsync();

        Assert.Single((await new ListUsersQueryHandler(db).Handle(new ListUsersQuery { Page = 1, PageSize = 10, Search = "Ruslan" }, default)).Value!.Results);
        Assert.Single((await new ListEventsQueryHandler(db).Handle(new ListEventsQuery { Page = 1, PageSize = 10, Search = "Технологии" }, default)).Value!.Results);
        Assert.Single((await new ListRegistrationsQueryHandler(db).Handle(new ListRegistrationsQuery { Page = 1, PageSize = 10, Search = "Draft" }, default)).Value!.Results);
        Assert.Single((await new ListEventProgramsQueryHandler(db).Handle(new ListEventProgramsQuery { Page = 1, PageSize = 10, Search = "Открытие" }, default)).Value!.Results);
        Assert.Single((await new ListEventSummariesQueryHandler(db).Handle(new ListEventSummariesQuery { Page = 1, PageSize = 10, Search = "итоги" }, default)).Value!.Results);
        Assert.Single((await new ListEventFilesQueryHandler(db).Handle(new ListEventFilesQuery { Page = 1, PageSize = 10, Search = "program" }, default)).Value!.Results);
        Assert.Single((await new ListInvoicesQueryHandler(db).Handle(new ListInvoicesQuery { Page = 1, PageSize = 10, Search = "INV" }, default)).Value!.Results);
        Assert.Single((await new ListPaymentsQueryHandler(db).Handle(new ListPaymentsQuery { Page = 1, PageSize = 10, Search = "REF" }, default)).Value!.Results);

        Assert.Single((await new ListUsersQueryHandler(db).Handle(new ListUsersQuery { Page = 1, PageSize = 10 }, default)).Value!.Results);
        Assert.Single((await new ListEventsQueryHandler(db).Handle(new ListEventsQuery { Page = 1, PageSize = 10 }, default)).Value!.Results);
    }
}
