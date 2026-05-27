using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class SaveEvent_SummaryCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string SummaryText { get; set; } = string.Empty;
    }
}