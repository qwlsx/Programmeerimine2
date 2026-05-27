#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    // Обязательно должен быть IRequest<OperationResult<Invoice>> или аналогичный
    public class GetInvoiceQuery : IRequest<OperationResult<Invoice?>>
    {
        public int Id { get; set; }
    }
}