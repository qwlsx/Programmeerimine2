using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.Payments;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.UnitTests.Features.Payments
{
    public class GetPaymentQueryHandlerTests
    {
        private readonly Mock<IPaymentRepository> _repositoryMock;
        private readonly GetPaymentQueryHandler _handler;

        public GetPaymentQueryHandlerTests()
        {
            _repositoryMock = new Mock<IPaymentRepository>();
            _handler = new GetPaymentQueryHandler(_repositoryMock.Object);
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
            var entity = new Payment { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            var query = new GetPaymentQuery { Id = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
        }
    }
}