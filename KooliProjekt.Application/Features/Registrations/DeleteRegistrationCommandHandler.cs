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
        private readonly KooliProjekt.Application.Data.Repositories.IRegistrationRepository _repository;

        public DeleteRegistrationCommandHandler(KooliProjekt.Application.Data.Repositories.IRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteRegistrationCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("Registration", "Регистрация не найдена");
            await _repository.DeleteAsync(entity);

            return result;
        }
    }
}
