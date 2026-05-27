using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    // Хендлер, который дергает файлы ивентов из БД
    public class ListEventFilesQueryHandler : IRequestHandler<ListEventFilesQuery, OperationResult<PagedResult<Event_Files>>>
    {
        private readonly ApplicationDbContext _context;

        public ListEventFilesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Event_Files>>> Handle(ListEventFilesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Event_Files>>();

            // Беру файлы из таблицы Event_Files, сортирую по Id и режу на страницы
            result.Value = await _context.Event_Files
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}