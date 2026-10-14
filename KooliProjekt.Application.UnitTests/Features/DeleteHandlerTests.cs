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
using Moq;

namespace KooliProjekt.Application.UnitTests.Features;

public class DeleteHandlerTests
{
    [Fact]
    public async Task Delete_user_should_use_repository()
    {
        var entity = new User { Id = 5 };
        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteUserCommandHandler(repository.Object).Handle(new DeleteUserCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_event_should_use_repository()
    {
        var entity = new Event { Id = 5 };
        var repository = new Mock<IEventRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteEventCommandHandler(repository.Object).Handle(new DeleteEventCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_registration_should_use_repository()
    {
        var entity = new Registration { Id = 5 };
        var repository = new Mock<IRegistrationRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteRegistrationCommandHandler(repository.Object).Handle(new DeleteRegistrationCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_program_should_use_repository()
    {
        var entity = new Event_Program { Id = 5 };
        var repository = new Mock<IEventProgramRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteEventProgramCommandHandler(repository.Object).Handle(new DeleteEventProgramCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_summary_should_use_repository()
    {
        var entity = new Event_Summary { Id = 5 };
        var repository = new Mock<IEventSummaryRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteEventSummaryCommandHandler(repository.Object).Handle(new DeleteEventSummaryCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_file_should_use_repository()
    {
        var entity = new Event_Files { Id = 5 };
        var repository = new Mock<IEventFileRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteEventFileCommandHandler(repository.Object).Handle(new DeleteEventFileCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_invoice_should_use_repository()
    {
        var entity = new Invoice { Id = 5 };
        var repository = new Mock<IInvoiceRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeleteInvoiceCommandHandler(repository.Object).Handle(new DeleteInvoiceCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Delete_payment_should_use_repository()
    {
        var entity = new Payment { Id = 5 };
        var repository = new Mock<IPaymentRepository>();
        repository.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(entity);
        var result = await new DeletePaymentCommandHandler(repository.Object).Handle(new DeletePaymentCommand { Id = 5 }, default);
        Assert.False(result.HasErrors);
        repository.Verify(x => x.DeleteAsync(entity), Times.Once);
    }
}
