#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult<User?>>
    {
        private readonly IUserRepository _repository;

        public GetUserQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<User?>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<User?>();

            if (request == null)
            {
                return result;
            }

            result.Value = await _repository.GetByIdAsync(request.Id);

            return result;
        }
    }
}