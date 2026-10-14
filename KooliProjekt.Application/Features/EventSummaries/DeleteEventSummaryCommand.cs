using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class DeleteEventSummaryCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
