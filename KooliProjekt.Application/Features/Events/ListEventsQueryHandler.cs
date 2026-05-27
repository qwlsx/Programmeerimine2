using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    // Хендлер, который обрабатывает мой запрос ListEventsQuery
    public class ListEventsQueryHandler : IRequestHandler<ListEventsQuery, OperationResult<PagedResult<Event>>>
    {
        private readonly ApplicationDbContext _context;

        // Подтягиваю контекст базы данных через конструктор
        public ListEventsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Event>>> Handle(ListEventsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Event>>();

            // Вытаскиваю данные из таблицы Events, сортирую по имени (Name) 
            // и вызываю метод расширения от учителя .GetPagedAsync, чтобы разбить на страницы
            result.Value = await _context.Events
                .OrderBy(x => x.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}