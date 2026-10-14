using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    // Хендлер для удаления счета из базы данных
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, OperationResult>
    {
        private readonly KooliProjekt.Application.Data.Repositories.IInvoiceRepository _repository;

        public DeleteInvoiceCommandHandler(KooliProjekt.Application.Data.Repositories.IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("Invoice", "Счёт не найден");
            await _repository.DeleteAsync(entity);

            return result;
        }
    }
}
