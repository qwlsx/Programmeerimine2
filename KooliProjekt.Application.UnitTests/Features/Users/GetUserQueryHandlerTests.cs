using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.Users;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.UnitTests.Features.Users
{
    public class GetUserQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly GetUserQueryHandler _handler;

        public GetUserQueryHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _handler = new GetUserQueryHandler(_repositoryMock.Object);
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
            var entity = new User { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            var query = new GetUserQuery { Id = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
        }
    }
}