#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories; // Добавляем репозиторий
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandHandler : IRequestHandler<SaveInvoiceCommand, OperationResult>
    {
        private readonly IInvoiceRepository _repository;

        public SaveInvoiceCommandHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveInvoiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Если Id 0, создаем новый, иначе ищем существующий
            var entity = request.Id == 0 ? new Invoice() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Invoice", "Счет не найден");
                return result;
            }

            // Обновляем поля
            entity.RegistrationId = request.RegistrationId;
            entity.Number = request.Id == 0 ? await _repository.GetNextNumberAsync() : request.Number;
            entity.TotalAmount = request.TotalAmount;
            entity.Status = request.Status;

            // Если это новый счет, задаем дату создания
            if (request.Id == 0)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            // Сохраняем через репозиторий
            await _repository.SaveAsync(entity);

            return result;
        }
    }
}
