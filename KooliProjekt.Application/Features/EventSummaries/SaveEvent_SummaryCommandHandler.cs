using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class SaveEvent_SummaryCommandHandler : IRequestHandler<SaveEvent_SummaryCommand, OperationResult>
    {
        private readonly IEventSummaryRepository _repository;

        public SaveEvent_SummaryCommandHandler(IEventSummaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveEvent_SummaryCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = request.Id == 0 ? new Event_Summary() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Event_Summary", "Сводка не найдена");
                return result;
            }

            entity.EventId = request.EventId;
            entity.SummaryText = request.SummaryText;

            await _repository.SaveAsync(entity);
            return result;
        }
    }
}
