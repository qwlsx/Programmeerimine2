using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    // Мой хендлер для вытаскивания расписания
    public class ListEventProgramsQueryHandler : IRequestHandler<ListEventProgramsQuery, OperationResult<PagedResult<Event_Program>>>
    {
        private readonly ApplicationDbContext _context;

        public ListEventProgramsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Event_Program>>> Handle(ListEventProgramsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Event_Program>>();

            // Достаю пункты программы из таблицы Event_Programs, сортирую по Id
            result.Value = await _context.Event_Programs
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}