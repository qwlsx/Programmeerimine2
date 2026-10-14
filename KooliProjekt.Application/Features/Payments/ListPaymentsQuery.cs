using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    // Мой запрос на получение страницы со списком платежей
    public class ListPaymentsQuery : IRequest<OperationResult<PagedResult<Payment>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public string Search { get; set; }
    }
}
