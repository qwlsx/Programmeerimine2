using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    // Хендлер для получения итогов из базы
    public class ListEventSummariesQueryHandler : IRequestHandler<ListEventSummariesQuery, OperationResult<PagedResult<Event_Summary>>>
    {
        private readonly ApplicationDbContext _context;

        public ListEventSummariesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Event_Summary>>> Handle(ListEventSummariesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Event_Summary>>();

            // Вытаскиваю итоги из таблицы Event_Summaries, сортирую по Id
            result.Value = await _context.Event_Summaries
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}