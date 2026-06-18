#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<Invoice?>>
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoiceQueryHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<Invoice?>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Invoice?>();

            if (request == null)
            {
                return result;
            }

            result.Value = await _repository.GetByIdAsync(request.Id);

            return result;
        }
    }
}