using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
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
using Moq;

namespace KooliProjekt.Application.UnitTests.Features;

public class SaveHandlerTests
{
    [Fact]
    public async Task Save_user_should_pass_entity_to_repository()
    {
        var repository = new Mock<IUserRepository>();
        var result = await new SaveUserCommandHandler(repository.Object).Handle(
            new SaveUserCommand { Name = "Ruslan", Email = "ruslan@test.ee", Role = "User" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<User>(u => u.Name == "Ruslan" && u.CreatedAt != default)), Times.Once);
    }

    [Fact]
    public async Task Save_event_should_pass_entity_to_repository()
    {
        var repository = new Mock<IEventRepository>();
        var result = await new SaveEventCommandHandler(repository.Object).Handle(
            new SaveEventCommand { Name = "Событие", StartDateTime = DateTime.UtcNow.AddDays(1), TotalSeats = 10 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Event>(e => e.Name == "Событие" && e.TotalSeats == 10)), Times.Once);
    }

    [Fact]
    public async Task Save_registration_should_pass_entity_to_repository()
    {
        var repository = new Mock<IRegistrationRepository>();
        var result = await new SaveRegistrationCommandHandler(repository.Object).Handle(
            new SaveRegistrationCommand { UserId = 1, EventId = 2, Status = "Draft" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Registration>(e => e.UserId == 1 && e.RegisteredAt != default)), Times.Once);
    }

    [Fact]
    public async Task Save_program_should_validate_event_and_use_repository()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        await using var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();
        var eventEntity = new Event { Name = "Событие", StartDateTime = DateTime.UtcNow.AddDays(1), TotalSeats = 10, CreatedAt = DateTime.UtcNow };
        dbContext.Events.Add(eventEntity);
        await dbContext.SaveChangesAsync();
        var repository = new Mock<IEventProgramRepository>();
        var result = await new SaveEvent_ProgramCommandHandler(dbContext, repository.Object).Handle(
            new SaveEvent_ProgramCommand { EventId = eventEntity.Id, Time = eventEntity.StartDateTime.AddHours(1), Topic = "Открытие" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Event_Program>(e => e.Topic == "Открытие")), Times.Once);
    }

    [Fact]
    public async Task Save_summary_should_pass_entity_to_repository()
    {
        var repository = new Mock<IEventSummaryRepository>();
        var result = await new SaveEvent_SummaryCommandHandler(repository.Object).Handle(
            new SaveEvent_SummaryCommand { EventId = 1, SummaryText = "Итоги" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Event_Summary>(e => e.SummaryText == "Итоги")), Times.Once);
    }

    [Fact]
    public async Task Save_file_should_pass_entity_to_repository()
    {
        var repository = new Mock<IEventFileRepository>();
        var result = await new SaveEvent_FileCommandHandler(repository.Object).Handle(
            new SaveEvent_FileCommand { EventId = 1, FileUrl = "https://example.test/file.pdf", FileType = "pdf" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Event_Files>(e => e.FileType == "pdf")), Times.Once);
    }

    [Fact]
    public async Task Save_invoice_should_pass_entity_to_repository()
    {
        var repository = new Mock<IInvoiceRepository>();
        var result = await new SaveInvoiceCommandHandler(repository.Object).Handle(
            new SaveInvoiceCommand { RegistrationId = 1, Number = "INV-200", TotalAmount = 20, Status = "Draft" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Invoice>(e => e.Number == "INV-200" && e.CreatedAt != default)), Times.Once);
    }

    [Fact]
    public async Task Save_payment_should_pass_entity_to_repository()
    {
        var repository = new Mock<IPaymentRepository>();
        await using var dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
        var result = await new SavePaymentCommandHandler(repository.Object, dbContext).Handle(
            new SavePaymentCommand { InvoiceId = 1, Amount = 20, PaymentMethod = "Bank", Status = "Processing" }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.SaveAsync(It.Is<Payment>(e => e.Amount == 20 && e.Status == "Processing")), Times.Once);
    }
}
