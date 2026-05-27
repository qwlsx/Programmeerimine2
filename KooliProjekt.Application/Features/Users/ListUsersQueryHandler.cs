using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    // Хендлер, который ловит мой запрос ListUsersQuery и лезет в БД
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, OperationResult<PagedResult<User>>>
    {
        private readonly ApplicationDbContext _context;

        // Подключаю контекст базы, чтобы работать с таблицами
        public ListUsersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<User>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<User>>();

            // Вытаскиваю данные из таблицы Users, сортирую по имени (Name)
            // и разбиваю на страницы методом от учителя .GetPagedAsync
            result.Value = await _context.Users
                .OrderBy(x => x.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}