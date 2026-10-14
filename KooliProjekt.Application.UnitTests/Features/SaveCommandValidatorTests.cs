using KooliProjekt.Application.Features.EventFiles;
using KooliProjekt.Application.Features.EventPrograms;
using KooliProjekt.Application.Features.Events;
using KooliProjekt.Application.Features.EventSummaries;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.Features.Payments;
using KooliProjekt.Application.Features.Registrations;
using KooliProjekt.Application.Features.Users;

namespace KooliProjekt.Application.UnitTests.Features;

public class SaveCommandValidatorTests
{
    [Fact]
    public void User_valid_command_should_pass() => Assert.True(new SaveUserCommandValidator().Validate(
        new SaveUserCommand { Name = "Ruslan", Email = "ruslan@test.ee", Role = "User" }).IsValid);

    [Fact]
    public void User_invalid_fields_should_fail()
    {
        var result = new SaveUserCommandValidator().Validate(new SaveUserCommand { Name = "", Email = "not-email", Role = "" });
        Assert.Contains(result.Errors, x => x.PropertyName == "Name");
        Assert.Contains(result.Errors, x => x.PropertyName == "Email");
        Assert.Contains(result.Errors, x => x.PropertyName == "Role");
    }

    [Fact]
    public void Event_valid_command_should_pass() => Assert.True(new SaveEventCommandValidator().Validate(
        new SaveEventCommand { Name = "Конференция", StartDateTime = DateTime.Now.AddDays(1), TotalSeats = 10, Price = 5 }).IsValid);

    [Fact]
    public void Event_invalid_fields_should_fail()
    {
        var result = new SaveEventCommandValidator().Validate(new SaveEventCommand { Name = "", StartDateTime = DateTime.Now.AddDays(-1), TotalSeats = 0, Price = -1 });
        Assert.Contains(result.Errors, x => x.PropertyName == "Name");
        Assert.Contains(result.Errors, x => x.PropertyName == "StartDateTime");
        Assert.Contains(result.Errors, x => x.PropertyName == "TotalSeats");
        Assert.Contains(result.Errors, x => x.PropertyName == "Price");
    }

    [Fact]
    public void Registration_valid_command_should_pass() => Assert.True(new SaveRegistrationCommandValidator().Validate(
        new SaveRegistrationCommand { UserId = 1, EventId = 1, Status = "Draft", RegisteredAt = DateTime.UtcNow }).IsValid);

    [Fact]
    public void Registration_invalid_fields_should_fail()
    {
        var result = new SaveRegistrationCommandValidator().Validate(new SaveRegistrationCommand { Status = "Unknown" });
        Assert.Contains(result.Errors, x => x.PropertyName == "UserId");
        Assert.Contains(result.Errors, x => x.PropertyName == "EventId");
        Assert.Contains(result.Errors, x => x.PropertyName == "Status");
        Assert.Contains(result.Errors, x => x.PropertyName == "RegisteredAt");
    }

    [Fact]
    public void Program_valid_command_should_pass() => Assert.True(new SaveEventProgramCommandValidator().Validate(
        new SaveEvent_ProgramCommand { EventId = 1, Time = DateTime.Now.AddDays(1), Topic = "Открытие" }).IsValid);

    [Fact]
    public void Program_invalid_fields_should_fail()
    {
        var result = new SaveEventProgramCommandValidator().Validate(new SaveEvent_ProgramCommand());
        Assert.Contains(result.Errors, x => x.PropertyName == "EventId");
        Assert.Contains(result.Errors, x => x.PropertyName == "Time");
        Assert.Contains(result.Errors, x => x.PropertyName == "Topic");
    }

    [Fact]
    public void Summary_valid_command_should_pass() => Assert.True(new SaveEventSummaryCommandValidator().Validate(
        new SaveEvent_SummaryCommand { EventId = 1, SummaryText = "Итоги" }).IsValid);

    [Fact]
    public void Summary_invalid_fields_should_fail()
    {
        var result = new SaveEventSummaryCommandValidator().Validate(new SaveEvent_SummaryCommand());
        Assert.Contains(result.Errors, x => x.PropertyName == "EventId");
        Assert.Contains(result.Errors, x => x.PropertyName == "SummaryText");
    }

    [Fact]
    public void File_valid_command_should_pass() => Assert.True(new SaveEventFileCommandValidator().Validate(
        new SaveEvent_FileCommand { EventId = 1, FileUrl = "https://example.test/file.pdf", FileType = "pdf" }).IsValid);

    [Fact]
    public void File_invalid_fields_should_fail()
    {
        var result = new SaveEventFileCommandValidator().Validate(new SaveEvent_FileCommand());
        Assert.Contains(result.Errors, x => x.PropertyName == "EventId");
        Assert.Contains(result.Errors, x => x.PropertyName == "FileUrl");
        Assert.Contains(result.Errors, x => x.PropertyName == "FileType");
    }

    [Fact]
    public void Invoice_valid_command_should_pass() => Assert.True(new SaveInvoiceCommandValidator().Validate(
        new SaveInvoiceCommand { RegistrationId = 1, Number = "INV-101", TotalAmount = 10, Status = "Draft" }).IsValid);

    [Fact]
    public void Invoice_invalid_fields_should_fail()
    {
        var result = new SaveInvoiceCommandValidator().Validate(new SaveInvoiceCommand { TotalAmount = -1, Status = "Unknown" });
        Assert.Contains(result.Errors, x => x.PropertyName == "RegistrationId");
        Assert.Contains(result.Errors, x => x.PropertyName == "Number");
        Assert.Contains(result.Errors, x => x.PropertyName == "TotalAmount");
        Assert.Contains(result.Errors, x => x.PropertyName == "Status");
    }

    [Fact]
    public void Payment_valid_command_should_pass() => Assert.True(new SavePaymentCommandValidator().Validate(
        new SavePaymentCommand { InvoiceId = 1, Amount = 10, PaymentMethod = "Bank", Status = "Completed", PaidAt = DateTime.UtcNow }).IsValid);

    [Fact]
    public void Payment_invalid_fields_should_fail()
    {
        var result = new SavePaymentCommandValidator().Validate(new SavePaymentCommand { Amount = -1, Status = "Completed" });
        Assert.Contains(result.Errors, x => x.PropertyName == "InvoiceId");
        Assert.Contains(result.Errors, x => x.PropertyName == "Amount");
        Assert.Contains(result.Errors, x => x.PropertyName == "PaymentMethod");
        Assert.Contains(result.Errors, x => x.PropertyName == "PaidAt");
    }
}
