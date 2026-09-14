using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.UnitTests.Features.Invoices
{
    public class GetInvoiceQueryHandlerTests
    {
        private readonly Mock<IInvoiceRepository> _repositoryMock;
        private readonly GetInvoiceQueryHandler _handler;

        public GetInvoiceQueryHandlerTests()
        {
            _repositoryMock = new Mock<IInvoiceRepository>();
            _handler = new GetInvoiceQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Act & Assert: проверяем, что при null выбрасывается ArgumentNullException
            Func<Task> act = async () => await _handler.Handle(null!, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Handle_ShouldReturnEmptyResult_WhenRequestIdIsZeroOrLess(int id)
        {
            // Arrange
            var query = new GetInvoiceQuery { Id = id };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            // Убеждаемся, что база/репозиторий НЕ вызывались
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResultWithValue_WhenEntityExists()
        {
            // Arrange
            var entity = new Invoice { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            var query = new GetInvoiceQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(1);

            _repositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }
    }
}