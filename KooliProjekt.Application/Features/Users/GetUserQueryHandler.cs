#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult<UserDto?>>
    {
        private readonly IUserRepository _repository;
        public GetUserQueryHandler(IUserRepository repository) => _repository = repository;

        public async Task<OperationResult<UserDto?>> Handle(GetUserQuery request, CancellationToken ct)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<UserDto?>();

            if (request.Id <= 0)
            {
                return result;
            }

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity != null)
            {
                result.Value = new UserDto { Id = entity.Id };
            }
            return result;
        }
    }
}