#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, OperationResult<PaymentDto?>>
    {
        private readonly IPaymentRepository _repository;
        public GetPaymentQueryHandler(IPaymentRepository repository) => _repository = repository;

        public async Task<OperationResult<PaymentDto?>> Handle(GetPaymentQuery request, CancellationToken ct)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<PaymentDto?>();

            if (request.Id <= 0)
            {
                return result;
            }

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity != null)
            {
                result.Value = new PaymentDto { Id = entity.Id };
            }
            return result;
        }
    }
}