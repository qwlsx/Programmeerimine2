#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<InvoiceDto?>>
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoiceQueryHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<InvoiceDto?>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            // 1. Если request null — кидаем ArgumentNullException
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<InvoiceDto?>();

            // 2. Если Id <= 0 — не обращаемся к репозиторию, сразу отдаем пустой result
            if (request.Id <= 0)
            {
                return result;
            }

            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity != null)
            {
                result.Value = new InvoiceDto
                {
                    Id = entity.Id
                };
            }

            return result;
        }
    }
}