using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.Registrations;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.UnitTests.Features.Registrations
{
    public class GetRegistrationQueryHandlerTests
    {
        private readonly Mock<IRegistrationRepository> _repositoryMock;
        private readonly GetRegistrationQueryHandler _handler;

        public GetRegistrationQueryHandlerTests()
        {
            _repositoryMock = new Mock<IRegistrationRepository>();
            _handler = new GetRegistrationQueryHandler(_repositoryMock.Object);
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
            var entity = new Registration { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            var query = new GetRegistrationQuery { Id = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
        }
    }
}