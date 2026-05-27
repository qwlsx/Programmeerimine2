using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments
{
    // Хендлер, который выдергивает платежи из базы
    public class ListPaymentsQueryHandler : IRequestHandler<ListPaymentsQuery, OperationResult<PagedResult<Payment>>>
    {
        private readonly ApplicationDbContext _context;

        public ListPaymentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Payment>>> Handle(ListPaymentsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Payment>>();

            // Беру платежи, сортирую по Id и бью на страницы методом учителя
            result.Value = await _context.Payments
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}