using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    // Моя команда для удаления счета
    public class DeleteInvoiceCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}