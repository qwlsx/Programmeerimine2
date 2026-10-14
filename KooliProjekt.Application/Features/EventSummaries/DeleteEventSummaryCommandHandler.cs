using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class DeleteEventSummaryCommandHandler : IRequestHandler<DeleteEventSummaryCommand, OperationResult>
    {
        private readonly IEventSummaryRepository _repository;
        public DeleteEventSummaryCommandHandler(IEventSummaryRepository repository) => _repository = repository;

        public async Task<OperationResult> Handle(DeleteEventSummaryCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("EventSummary", "Итоги мероприятия не найдены");
            await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
