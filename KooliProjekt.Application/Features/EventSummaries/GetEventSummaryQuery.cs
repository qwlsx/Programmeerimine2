using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class GetEventSummaryQuery : IRequest<OperationResult<EventSummaryDto>>
    {
        public int Id { get; set; }
    }
}
