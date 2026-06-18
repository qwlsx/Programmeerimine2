using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.ToDoLists;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using System.Collections.Generic;

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
        public async Task Handle_ShouldReturnEmptyResult_WhenRequestIsNull()
        {
            var result = await _handler.Handle(null!, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().BeNull();
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
        }
    }
}