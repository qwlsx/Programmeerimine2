using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class GetEventFileQueryHandler : IRequestHandler<GetEventFileQuery, OperationResult<EventFileDto>>
    {
        private readonly IEventFileRepository _repository;
        public GetEventFileQueryHandler(IEventFileRepository repository) => _repository = repository;

        public async Task<OperationResult<EventFileDto>> Handle(GetEventFileQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = new OperationResult<EventFileDto>();
            if (request.Id <= 0) return result;
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result;
            result.Value = new EventFileDto { Id = entity.Id, EventId = entity.EventId, FileUrl = entity.FileUrl, FileType = entity.FileType };
            return result;
        }
    }
}
