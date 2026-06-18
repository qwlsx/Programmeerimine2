#nullable enable
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    // Меняем OperationResult<Invoice?> на OperationResult<InvoiceDto?>
    public class GetInvoiceQuery : IRequest<OperationResult<InvoiceDto?>>
    {
        public int Id { get; set; }
    }
}