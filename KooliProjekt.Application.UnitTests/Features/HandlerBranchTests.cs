using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.EventFiles;
using KooliProjekt.Application.Features.Events;
using KooliProjekt.Application.Features.EventSummaries;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.Features.Payments;
using KooliProjekt.Application.Features.Registrations;
using KooliProjekt.Application.Features.Users;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace KooliProjekt.Application.UnitTests.Features;

public class HandlerBranchTests
{
    [Fact]
    public async Task Get_event_should_map_entity_and_return_null_when_missing()
    {
        var repository = new Mock<IEventRepository>();
        repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Event { Id = 1, Name = "Конференция", TotalSeats = 10 });
        var handler = new GetEventQueryHandler(repository.Object);
        Assert.Equal("Конференция", (await handler.Handle(new GetEventQuery { Id = 1 }, default)).Value!.Name);
        Assert.Null((await handler.Handle(new GetEventQuery { Id = 2 }, default)).Value);
    }

    [Fact]
    public async Task Save_repository_handlers_should_report_missing_entities()
    {
        var user = await new SaveUserCommandHandler(new Mock<IUserRepository>().Object).Handle(new SaveUserCommand { Id = 99 }, default);
        var registration = await new SaveRegistrationCommandHandler(new Mock<IRegistrationRepository>().Object).Handle(new SaveRegistrationCommand { Id = 99 }, default);
        var summary = await new SaveEvent_SummaryCommandHandler(new Mock<IEventSummaryRepository>().Object).Handle(new SaveEvent_SummaryCommand { Id = 99 }, default);
        var file = await new SaveEvent_FileCommandHandler(new Mock<IEventFileRepository>().Object).Handle(new SaveEvent_FileCommand { Id = 99 }, default);
        var invoice = await new SaveInvoiceCommandHandler(new Mock<IInvoiceRepository>().Object).Handle(new SaveInvoiceCommand { Id = 99 }, default);
        Assert.True(user.HasErrors);
        Assert.True(registration.HasErrors);
        Assert.True(summary.HasErrors);
        Assert.True(file.HasErrors);
        Assert.True(invoice.HasErrors);
    }

    [Fact]
    public async Task Save_event_should_report_missing_and_invalid_date()
    {
        var repository = new Mock<IEventRepository>();
        var handler = new SaveEventCommandHandler(repository.Object);
        Assert.True((await handler.Handle(new SaveEventCommand { Id = 99 }, default)).HasErrors);

        repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Event { Id = 1, CreatedAt = DateTime.UtcNow.AddDays(1) });
        var result = await handler.Handle(new SaveEventCommand { Id = 1, StartDateTime = DateTime.UtcNow }, default);
        Assert.True(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.IsAny<Event>()), Times.Never);
    }

    [Fact]
    public async Task Save_program_should_report_missing_program_event_and_early_time()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        await using var db = new ApplicationDbContext(options);
        await db.Database.EnsureCreatedAsync();
        var repository = new Mock<IEventProgramRepository>();
        var handler = new SaveEvent_ProgramCommandHandler(db, repository.Object);
        Assert.True((await handler.Handle(new SaveEvent_ProgramCommand { Id = 99 }, default)).HasErrors);
        Assert.True((await handler.Handle(new SaveEvent_ProgramCommand { EventId = 999, Time = DateTime.UtcNow }, default)).HasErrors);

        var eventEntity = new Event { Name = "Событие", CreatedAt = DateTime.UtcNow, StartDateTime = DateTime.UtcNow.AddDays(2), TotalSeats = 10 };
        db.Events.Add(eventEntity);
        await db.SaveChangesAsync();
        var result = await handler.Handle(new SaveEvent_ProgramCommand { EventId = eventEntity.Id, Time = eventEntity.StartDateTime.AddMinutes(-1) }, default);
        Assert.True(result.HasErrors);
    }

    [Fact]
    public async Task Completed_payment_should_finalize_invoice_and_registration()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        await using var db = new ApplicationDbContext(options);
        await db.Database.EnsureCreatedAsync();
        var user = new User { Name = "User", Email = "payment@test.ee", Role = "User", CreatedAt = DateTime.UtcNow };
        var eventEntity = new Event { Name = "Event", CreatedAt = DateTime.UtcNow, StartDateTime = DateTime.UtcNow.AddDays(1), TotalSeats = 10 };
        db.AddRange(user, eventEntity);
        await db.SaveChangesAsync();
        var registration = new Registration { UserId = user.Id, EventId = eventEntity.Id, Status = "Draft", RegisteredAt = DateTime.UtcNow };
        db.Registrations.Add(registration);
        await db.SaveChangesAsync();
        var invoice = new Invoice { RegistrationId = registration.Id, Number = "INV-COMPLETE", TotalAmount = 20, Status = "Draft", CreatedAt = DateTime.UtcNow };
        db.Invoices.Add(invoice);
        await db.SaveChangesAsync();

        var repository = new Mock<IPaymentRepository>();
        var handler = new SavePaymentCommandHandler(repository.Object, db);
        var result = await handler.Handle(new SavePaymentCommand { InvoiceId = invoice.Id, Amount = 20, PaymentMethod = "Bank", Status = "Completed", PaidAt = DateTime.UtcNow }, default);
        Assert.False(result.HasErrors);
        Assert.Equal("Final", invoice.Status);
        Assert.Equal("Paid", registration.Status);

        var missing = await handler.Handle(new SavePaymentCommand { InvoiceId = 999, Status = "Completed" }, default);
        Assert.False(missing.HasErrors);
        var notFound = await handler.Handle(new SavePaymentCommand { Id = 99 }, default);
        Assert.True(notFound.HasErrors);
    }
}
