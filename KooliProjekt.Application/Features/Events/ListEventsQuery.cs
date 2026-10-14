using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    public class ListEventsQuery : IRequest<OperationResult<PagedResult<EventDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public string Search { get; set; }
    }
}
