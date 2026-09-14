using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.ToDoLists;

namespace KooliProjekt.Application.UnitTests.Features.ToDoLists
{
    public class ListToDoListsQueryHandlerTests
    {
        private readonly ListToDoListsQueryHandler _handler;

        public ListToDoListsQueryHandlerTests()
        {
            // Передаем null, так как до работы с DbContext дело дойти не должно из-за валидации
            _handler = new ListToDoListsQueryHandler(null!);
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
        public async Task Handle_ShouldThrowArgumentException_WhenPageIsZeroOrLess(int page)
        {
            var query = new ListToDoListsQuery { Page = page, PageSize = 10 };

            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Handle_ShouldThrowArgumentException_WhenPageSizeIsZeroOrLess(int pageSize)
        {
            var query = new ListToDoListsQuery { Page = 1, PageSize = pageSize };

            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenPageSizeExceedsLimit()
        {
            var query = new ListToDoListsQuery { Page = 1, PageSize = 101 };

            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }
    }
}