#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    public class GetPaymentQuery : IRequest<OperationResult<Payment?>>
    {
        public int Id { get; set; }
    }
}