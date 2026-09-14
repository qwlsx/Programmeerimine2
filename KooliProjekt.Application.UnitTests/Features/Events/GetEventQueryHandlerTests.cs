using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Events;
using Moq;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Events
{
    public class GetEventQueryHandlerTests
    {
        private readonly Mock<IEventRepository> _repositoryMock;
        private readonly GetEventQueryHandler _handler;

        public GetEventQueryHandlerTests()
        {
            _repositoryMock = new Mock<IEventRepository>();
            _handler = new GetEventQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            Func<Task> act = async () => await _handler.Handle(null!, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task Handle_ShouldReturnEmptyResultAndNotCallRepository_WhenIdIsZeroOrLess(int id)
        {
            var query = new GetEventQuery { Id = id };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().BeNull();
            _repositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }
    }
}