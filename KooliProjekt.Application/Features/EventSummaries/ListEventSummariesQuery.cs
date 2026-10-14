using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventSummaries
{
    // Запрос на постраничные итоги мероприятий с правильным типом Event_Summary
    public class ListEventSummariesQuery : IRequest<OperationResult<PagedResult<Event_Summary>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public string Search { get; set; }
    }
}
