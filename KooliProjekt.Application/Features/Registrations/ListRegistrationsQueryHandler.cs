using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    // Хендлер, который достает куски регистраций из базы
    public class ListRegistrationsQueryHandler : IRequestHandler<ListRegistrationsQuery, OperationResult<PagedResult<Registration>>>
    {
        private readonly ApplicationDbContext _context;

        // Инициализирую контекст БД через конструктор
        public ListRegistrationsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Registration>>> Handle(ListRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Registration>>();

            // Беру регистрации, сортирую по Id (так как текстового имени тут нет)
            // и вызываю постраничный вывод от учителя
            result.Value = await _context.Registrations
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}