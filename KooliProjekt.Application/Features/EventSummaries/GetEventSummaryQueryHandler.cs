using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class GetEventSummaryQueryHandler : IRequestHandler<GetEventSummaryQuery, OperationResult<EventSummaryDto>>
    {
        private readonly IEventSummaryRepository _repository;
        public GetEventSummaryQueryHandler(IEventSummaryRepository repository) => _repository = repository;

        public async Task<OperationResult<EventSummaryDto>> Handle(GetEventSummaryQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = new OperationResult<EventSummaryDto>();
            if (request.Id <= 0) return result;
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result;
            result.Value = new EventSummaryDto { Id = entity.Id, EventId = entity.EventId, SummaryText = entity.SummaryText };
            return result;
        }
    }
}
