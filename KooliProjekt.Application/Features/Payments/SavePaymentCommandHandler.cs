#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories; // Обязательный using для репозитория
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    public class SavePaymentCommandHandler : IRequestHandler<SavePaymentCommand, OperationResult>
    {
        private readonly IPaymentRepository _repository;

        public SavePaymentCommandHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SavePaymentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Если Id 0, создаем новый, иначе ищем через репозиторий
            var entity = request.Id == 0 ? new Payment() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Payment", "Платеж не найден");
                return result;
            }

            // Обновляем поля
            entity.InvoiceId = request.InvoiceId;
            entity.Amount = request.Amount;
            entity.PaymentMethod = request.PaymentMethod;
            entity.PaymentRef = request.PaymentRef;
            entity.Status = request.Status;
            entity.PaidAt = request.PaidAt;

            // Сохраняем через репозиторий
            await _repository.SaveAsync(entity);

            return result;
        }
    }
}