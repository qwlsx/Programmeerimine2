using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    // Мой запрос на вытаскивание страницы со счетами
    public class ListInvoicesQuery : IRequest<OperationResult<PagedResult<Invoice>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public string Search { get; set; }
    }
}
