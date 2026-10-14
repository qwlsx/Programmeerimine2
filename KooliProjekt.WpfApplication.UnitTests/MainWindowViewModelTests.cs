using KooliProjekt.WpfApplication.Api;
using Moq;

namespace KooliProjekt.WpfApplication.UnitTests;

public class MainWindowViewModelTests
{
    private readonly Mock<IApiClient> _api = new();
    private readonly Mock<IDialogProvider> _dialogs = new();

    [Fact]
    public async Task LoadData_should_fill_observable_collection()
    {
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>>
        {
            Value = new() { Results = new() { new() { Id = 1, Name = "Конференция" } } }
        });
        var model = CreateModel();
        await model.LoadDataAsync();
        Assert.Single(model.Events);
        Assert.Equal("Конференция", model.SelectedEvent!.Name);
    }

    [Fact]
    public async Task LoadData_should_show_error_with_faulty_response()
    {
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>> { Errors = new() { "Ошибка" } });
        await CreateModel().LoadDataAsync();
        _dialogs.Verify(x => x.ShowError("Ошибка"), Times.Once);
    }

    [Fact]
    public void AddNew_should_add_and_select_new_event()
    {
        var model = CreateModel();
        model.AddNew();
        Assert.Single(model.Events);
        Assert.Same(model.Events[0], model.SelectedEvent);
        Assert.Equal(1, model.SelectedEvent!.TotalSeats);
    }

    [Fact]
    public async Task Save_should_reload_data_after_success()
    {
        var model = CreateModel();
        model.AddNew();
        _api.Setup(x => x.SaveAsync(It.IsAny<EventModel>())).ReturnsAsync(new OperationResult());
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>> { Value = new() });
        await model.SaveAsync();
        _api.Verify(x => x.SaveAsync(It.IsAny<EventModel>()), Times.Once);
        _api.Verify(x => x.ListAsync(), Times.Once);
    }

    [Fact]
    public async Task Save_should_show_api_error()
    {
        var model = CreateModel();
        model.AddNew();
        _api.Setup(x => x.SaveAsync(It.IsAny<EventModel>())).ReturnsAsync(new OperationResult { PropertyErrors = new() { ["Name"] = "Название обязательно" } });
        await model.SaveAsync();
        _dialogs.Verify(x => x.ShowError("Название обязательно"), Times.Once);
    }

    [Fact]
    public async Task Delete_should_return_when_not_confirmed()
    {
        var model = CreateModel();
        model.Events.Add(new EventModel { Id = 4 });
        model.SelectedEvent = model.Events[0];
        _dialogs.Setup(x => x.ConfirmDelete()).Returns(false);
        await model.DeleteAsync();
        _api.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Delete_should_reload_after_success()
    {
        var model = CreateModel();
        model.Events.Add(new EventModel { Id = 4 });
        model.SelectedEvent = model.Events[0];
        _dialogs.Setup(x => x.ConfirmDelete()).Returns(true);
        _api.Setup(x => x.DeleteAsync(4)).ReturnsAsync(new OperationResult());
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>> { Value = new() });
        await model.DeleteAsync();
        _api.Verify(x => x.DeleteAsync(4), Times.Once);
        _api.Verify(x => x.ListAsync(), Times.Once);
    }

    private MainWindowViewModel CreateModel() => new(_api.Object, _dialogs.Object);
}
