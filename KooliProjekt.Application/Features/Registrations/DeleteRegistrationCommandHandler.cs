using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Registrations
{
    // Хендлер для удаления регистрации из базы
    public class DeleteRegistrationCommandHandler : IRequestHandler<DeleteRegistrationCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteRegistrationCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteRegistrationCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Удаляю регистрацию по ID
            await _dbContext.Registrations
                .Where(r => r.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}