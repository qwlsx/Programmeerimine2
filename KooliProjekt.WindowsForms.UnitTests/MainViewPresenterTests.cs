using KooliProjekt.WindowsForms.Api;
using Moq;

namespace KooliProjekt.WindowsForms.UnitTests;

public class MainViewPresenterTests
{
    private readonly Mock<IMainView> _view = new();
    private readonly Mock<IApiClient> _api = new();

    [Fact]
    public async Task LoadData_should_call_ShowError_with_faulty_response()
    {
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>> { Errors = new() { "Ошибка" } });
        await CreatePresenter().LoadData();
        _view.Verify(x => x.ShowError("Ошибка"), Times.Once);
    }

    [Fact]
    public async Task LoadData_should_set_DataSource_with_valid_response()
    {
        var events = new List<EventModel> { new() { Id = 1, Name = "Конференция" } };
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>> { Value = new() { Results = events } });
        await CreatePresenter().LoadData();
        _view.VerifySet(x => x.DataSource = events, Times.Once);
    }

    [Fact]
    public void SetSelection_should_clear_fields_with_null_selection()
    {
        CreatePresenter().SetSelection(null);
        _view.VerifySet(x => x.CurrentId = 0);
        _view.VerifySet(x => x.CurrentName = string.Empty);
        _view.VerifySet(x => x.CurrentTotalSeats = 1);
    }

    [Fact]
    public void SetSelection_should_set_fields_with_valid_selection()
    {
        var model = new EventModel { Id = 7, Name = "Выставка", TotalSeats = 50, Price = 12 };
        CreatePresenter().SetSelection(model);
        _view.VerifySet(x => x.CurrentId = 7);
        _view.VerifySet(x => x.CurrentName = "Выставка");
        _view.VerifySet(x => x.CurrentPrice = 12);
    }

    [Fact]
    public async Task Save_should_call_ShowError_with_faulty_response()
    {
        SetupFormValues();
        _api.Setup(x => x.SaveAsync(It.IsAny<EventModel>())).ReturnsAsync(new OperationResult { Errors = new() { "Ошибка" } });
        await CreatePresenter().Save();
        _view.Verify(x => x.ShowError("Ошибка"), Times.Once);
    }

    [Fact]
    public async Task Save_should_call_LoadData_with_valid_response()
    {
        SetupFormValues();
        _api.Setup(x => x.SaveAsync(It.IsAny<EventModel>())).ReturnsAsync(new OperationResult());
        _api.Setup(x => x.ListAsync()).ReturnsAsync(new OperationResult<PagedResult<EventModel>> { Value = new() });
        await CreatePresenter().Save();
        _api.Verify(x => x.ListAsync(), Times.Once);
    }

    [Fact]
    public async Task Delete_should_return_when_user_didnot_confirmed()
    {
        _view.SetupGet(x => x.CurrentId).Returns(5);
        _view.Setup(x => x.ConfirmDelete()).Returns(false);
        await CreatePresenter().Delete();
        _api.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Delete_should_call_ShowError_with_faulty_response()
    {
        _view.SetupGet(x => x.CurrentId).Returns(5);
        _view.Setup(x => x.ConfirmDelete()).Returns(true);
        _api.Setup(x => x.DeleteAsync(5)).ReturnsAsync(new OperationResult { Errors = new() { "Ошибка" } });
        await CreatePresenter().Delete();
        _view.Verify(x => x.ShowError("Ошибка"), Times.Once);
    }

    private MainViewPresenter CreatePresenter() => new(_view.Object, _api.Object);

    private void SetupFormValues()
    {
        _view.SetupGet(x => x.CurrentName).Returns("Событие");
        _view.SetupGet(x => x.CurrentStartDateTime).Returns(DateTime.Now.AddDays(1));
        _view.SetupGet(x => x.CurrentTotalSeats).Returns(10);
    }
}
