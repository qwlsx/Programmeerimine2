using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Payments
{
    // Хендлер для удаления платежа из базы данных
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, OperationResult>
    {
        private readonly KooliProjekt.Application.Data.Repositories.IPaymentRepository _repository;

        public DeletePaymentCommandHandler(KooliProjekt.Application.Data.Repositories.IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("Payment", "Платёж не найден");
            await _repository.DeleteAsync(entity);

            return result;
        }
    }
}
