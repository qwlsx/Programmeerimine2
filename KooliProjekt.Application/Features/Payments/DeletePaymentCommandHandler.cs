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
        private readonly ApplicationDbContext _dbContext;

        public DeletePaymentCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Удаляю платеж по его ID
            await _dbContext.Payments
                .Where(p => p.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}