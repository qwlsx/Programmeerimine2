using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.EventPrograms;

namespace KooliProjekt.Application.UnitTests.Features.EventPrograms
{
    public class ListEventProgramsQueryHandlerTests
    {
        private readonly ListEventProgramsQueryHandler _handler;

        public ListEventProgramsQueryHandlerTests()
        {
            _handler = new ListEventProgramsQueryHandler(null!);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            Func<Task> act = async () =>
                await _handler.Handle(null!, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Handle_ShouldThrowArgumentException_WhenPageIsZeroOrLess(
            int page)
        {
            var query = new ListEventProgramsQuery
            {
                Page = page,
                PageSize = 10
            };

            Func<Task> act = async () =>
                await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Handle_ShouldThrowArgumentException_WhenPageSizeIsZeroOrLess(
            int pageSize)
        {
            var query = new ListEventProgramsQuery
            {
                Page = 1,
                PageSize = pageSize
            };

            Func<Task> act = async () =>
                await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenPageSizeExceedsLimit()
        {
            var query = new ListEventProgramsQuery
            {
                Page = 1,
                PageSize = 101
            };

            Func<Task> act = async () =>
                await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }
    }
}