using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    public class GetEventProgramQueryHandler : IRequestHandler<GetEventProgramQuery, OperationResult<EventProgramDto>>
    {
        private readonly IEventProgramRepository _repository;
        public GetEventProgramQueryHandler(IEventProgramRepository repository) => _repository = repository;

        public async Task<OperationResult<EventProgramDto>> Handle(GetEventProgramQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = new OperationResult<EventProgramDto>();
            if (request.Id <= 0) return result;
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result;
            result.Value = new EventProgramDto { Id = entity.Id, EventId = entity.EventId, Time = entity.Time, Topic = entity.Topic };
            return result;
        }
    }
}
