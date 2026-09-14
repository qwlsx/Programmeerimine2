#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    public class GetRegistrationQueryHandler : IRequestHandler<GetRegistrationQuery, OperationResult<RegistrationDto?>>
    {
        private readonly IRegistrationRepository _repository;
        public GetRegistrationQueryHandler(IRegistrationRepository repository) => _repository = repository;

        public async Task<OperationResult<RegistrationDto?>> Handle(GetRegistrationQuery request, CancellationToken ct)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<RegistrationDto?>();

            if (request.Id <= 0)
            {
                return result;
            }

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity != null)
            {
                result.Value = new RegistrationDto { Id = entity.Id };
            }
            return result;
        }
    }
}