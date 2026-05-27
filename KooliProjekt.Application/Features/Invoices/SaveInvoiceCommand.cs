#nullable enable
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public string Number { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}