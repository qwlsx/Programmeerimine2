using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoicesQueryHandler : IRequestHandler<ListInvoicesQuery, OperationResult<PagedResult<Invoice>>>
    {
        private readonly ApplicationDbContext _context;

        public ListInvoicesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Invoice>>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0)
            {
                throw new ArgumentException("Page must be greater than zero.", nameof(request.Page));
            }

            if (request.PageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than zero.", nameof(request.PageSize));
            }

            if (request.PageSize > 100)
            {
                throw new ArgumentException("PageSize cannot exceed 100.", nameof(request.PageSize));
            }

            var result = new OperationResult<PagedResult<Invoice>>();

            result.Value = await _context.Invoices
                .OrderBy(x => x.Number)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}