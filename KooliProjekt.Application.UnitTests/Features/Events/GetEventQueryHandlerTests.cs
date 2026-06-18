using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.Events;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.UnitTests.Features.Events
{
    public class GetEventQueryHandlerTests
    {
        private readonly Mock<IEventRepository> _repositoryMock;
        private readonly GetEventQueryHandler _handler;

        public GetEventQueryHandlerTests()
        {
            // Создаем заглушку репозитория
            _repositoryMock = new Mock<IEventRepository>();
            _handler = new GetEventQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyResult_WhenRequestIsNull()
        {
            // Act
            var result = await _handler.Handle(null, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnResultWithValue_WhenEventExists()
        {
            // Arrange
            var eventEntity = new Event { Id = 1, Name = "Concert", TotalSeats = 100, Price = 50 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(eventEntity);

            var query = new GetEventQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
        }
    }
}