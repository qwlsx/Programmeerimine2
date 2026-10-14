using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    // Мой запрос на получение страницы со списком пользователей
    public class ListUsersQuery : IRequest<OperationResult<PagedResult<User>>>
    {
        // Номер страницы, которую я хочу открыть
        public int Page { get; set; } = 1;

        // Сколько пользователей показать на одной странице
        public int PageSize { get; set; } = 100;
        public string Search { get; set; }
    }
}
