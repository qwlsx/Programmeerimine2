using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.ToDoItems;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.UnitTests.Features.ToDoItems
{
    public class GetToDoItemQueryHandlerTests
    {
        private readonly Mock<IToDoItemRepository> _repositoryMock;
        private readonly GetToDoItemQueryHandler _handler;

        public GetToDoItemQueryHandlerTests()
        {
            _repositoryMock = new Mock<IToDoItemRepository>();
            _handler = new GetToDoItemQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyResult_WhenRequestIsNull()
        {
            var result = await _handler.Handle(null!, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnResultWithValue_WhenEntityExists()
        {
            var entity = new ToDoItem { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            var query = new GetToDoItemQuery { Id = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
        }
    }
}