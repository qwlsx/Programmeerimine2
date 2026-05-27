#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    public class GetRegistrationQueryHandler : IRequestHandler<GetRegistrationQuery, OperationResult<Registration?>>
    {
        private readonly IRegistrationRepository _repository;

        public GetRegistrationQueryHandler(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<Registration?>> Handle(GetRegistrationQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Registration?>();
            result.Value = await _repository.GetByIdAsync(request.Id);
            return result;
        }
    }
}