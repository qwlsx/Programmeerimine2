using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    // Мой запрос на получение страницы со списком мероприятий
    public class ListEventsQuery : IRequest<OperationResult<PagedResult<Event>>>
    {
        // Какую страницу по счету хочу получить
        public int Page { get; set; }

        // Сколько штук выводить на одной странице
        public int PageSize { get; set; }
    }
}