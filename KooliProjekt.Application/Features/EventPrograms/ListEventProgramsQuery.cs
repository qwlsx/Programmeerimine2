using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    // Мой запрос на список элементов программы мероприятий с правильным именем класса
    public class ListEventProgramsQuery : IRequest<OperationResult<PagedResult<Event_Program>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public string Search { get; set; }
    }
}
