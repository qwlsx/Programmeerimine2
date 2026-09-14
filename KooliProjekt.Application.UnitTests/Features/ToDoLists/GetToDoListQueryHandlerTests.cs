using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.ToDoLists;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.UnitTests.Features.ToDoLists
{
    public class GetToDoListQueryHandlerTests
    {
        private readonly Mock<IToDoListRepository> _repositoryMock;
        private readonly GetToDoListQueryHandler _handler;

        public GetToDoListQueryHandlerTests()
        {
            _repositoryMock = new Mock<IToDoListRepository>();
            _handler = new GetToDoListQueryHandler(_repositoryMock.Object);
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
        [InlineData(-10)]
        public async Task Handle_ShouldReturnEmptyResult_WhenRequestIdIsZeroOrLess(int id)
        {
            var query = new GetToDoListQuery { Id = id };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().BeNull();
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResultWithValue_WhenEntityExists()
        {
            var entity = new ToDoList { Id = 1, Title = "Test", Items = new List<ToDoItem>() };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            var query = new GetToDoListQuery { Id = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(1);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }
    }
}