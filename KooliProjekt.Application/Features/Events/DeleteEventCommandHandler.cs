using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Events
{
    // Хендлер, который выполняет удаление записи в базе
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, OperationResult>
    {
        private readonly KooliProjekt.Application.Data.Repositories.IEventRepository _repository;

        public DeleteEventCommandHandler(KooliProjekt.Application.Data.Repositories.IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("Event", "Мероприятие не найдено");
            await _repository.DeleteAsync(entity);

            return result;
        }
    }
}
