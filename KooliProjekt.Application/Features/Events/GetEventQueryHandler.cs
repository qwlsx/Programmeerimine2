using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, OperationResult<object>>
    {
        private readonly IEventRepository _repository;

        public GetEventQueryHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<object>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null) return result;

            result.Value = new
            {
                Id = entity.Id,
                Name = entity.Name,
                StartDateTime = entity.StartDateTime,
                Description = entity.Description,
                TotalSeats = entity.TotalSeats,
                Price = entity.Price
            };

            return result;
        }
    }
}