using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    // Моя команда для удаления платежа
    public class DeletePaymentCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}