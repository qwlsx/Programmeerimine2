using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.EventFiles;
using KooliProjekt.Application.Features.EventPrograms;
using KooliProjekt.Application.Features.EventSummaries;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace KooliProjekt.Application.UnitTests.Features;

public class EventChildrenQueryHandlerTests
{
    [Fact]
    public async Task Get_program_should_map_dto()
    {
        var repository = new Mock<IEventProgramRepository>();
        repository.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(new Event_Program { Id = 3, EventId = 2, Topic = "Доклад" });
        var result = await new GetEventProgramQueryHandler(repository.Object).Handle(new GetEventProgramQuery { Id = 3 }, default);
        Assert.Equal("Доклад", result.Value!.Topic);
    }

    [Fact]
    public async Task Get_summary_should_map_dto()
    {
        var repository = new Mock<IEventSummaryRepository>();
        repository.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(new Event_Summary { Id = 3, EventId = 2, SummaryText = "Итоги" });
        var result = await new GetEventSummaryQueryHandler(repository.Object).Handle(new GetEventSummaryQuery { Id = 3 }, default);
        Assert.Equal("Итоги", result.Value!.SummaryText);
    }

    [Fact]
    public async Task Get_file_should_map_dto()
    {
        var repository = new Mock<IEventFileRepository>();
        repository.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(new Event_Files { Id = 3, EventId = 2, FileUrl = "file.pdf", FileType = "pdf" });
        var result = await new GetEventFileQueryHandler(repository.Object).Handle(new GetEventFileQuery { Id = 3 }, default);
        Assert.Equal("file.pdf", result.Value!.FileUrl);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Get_children_should_not_call_repository_with_invalid_id(int id)
    {
        var repository = new Mock<IEventSummaryRepository>();
        var result = await new GetEventSummaryQueryHandler(repository.Object).Handle(new GetEventSummaryQuery { Id = id }, default);
        Assert.Null(result.Value);
        repository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task List_summaries_should_page_and_search()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        await using var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();
        var eventEntity = new Event { Name = "Событие", StartDateTime = DateTime.UtcNow.AddDays(1), TotalSeats = 10, CreatedAt = DateTime.UtcNow };
        dbContext.Events.Add(eventEntity);
        await dbContext.SaveChangesAsync();
        dbContext.Event_Summaries.AddRange(
            new Event_Summary { EventId = eventEntity.Id, SummaryText = "Нужные итоги" });
        await dbContext.SaveChangesAsync();
        var result = await new ListEventSummariesQueryHandler(dbContext).Handle(
            new ListEventSummariesQuery { Page = 1, PageSize = 10, Search = "Нужные" }, default);
        Assert.Single(result.Value!.Results);
    }
}
