#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.DTOs; // Подключаем DTO
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
            var result = new OperationResult<InvoiceDto?>();

            if (request == null)
            {
                return result;
            }

            var entity = await _repository.GetByIdAsync(request.Id);

            // Если сущность найдена, перекладываем данные в DTO
            if (entity != null)
            {
                result.Value = new InvoiceDto
                {
                    Id = entity.Id
                    // TODO: Переложи остальные свойства
                    // InvoiceNumber = entity.InvoiceNumber,
                    // Amount = entity.Amount
                };
            }

            return result;
        }
    }
}