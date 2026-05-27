using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class SaveEvent_SummaryCommandHandler : IRequestHandler<SaveEvent_SummaryCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveEvent_SummaryCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveEvent_SummaryCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = request.Id == 0 ? new Event_Summary() : await _dbContext.Event_Summaries.FindAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Event_Summary", "Сводка не найдена");
                return result;
            }

            entity.EventId = request.EventId;
            entity.SummaryText = request.SummaryText;

            if (request.Id == 0)
            {
                _dbContext.Event_Summaries.Add(entity);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}