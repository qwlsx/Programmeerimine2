#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, OperationResult<Payment?>>
    {
        private readonly IPaymentRepository _repository;

        public GetPaymentQueryHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<Payment?>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Payment?>();

            if (request == null)
            {
                return result;
            }

            result.Value = await _repository.GetByIdAsync(request.Id);

            return result;
        }
    }
}