using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    // Хендлер, который лезет в базу за инвойсами
    public class ListInvoicesQueryHandler : IRequestHandler<ListInvoicesQuery, OperationResult<PagedResult<Invoice>>>
    {
        private readonly ApplicationDbContext _context;

        public ListInvoicesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Invoice>>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Invoice>>();

            // Беру инвойсы, сортирую по их номеру (Number), чтобы список был красивым,
            // и пилю на страницы методом учителя
            result.Value = await _context.Invoices
                .OrderBy(x => x.Number)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}