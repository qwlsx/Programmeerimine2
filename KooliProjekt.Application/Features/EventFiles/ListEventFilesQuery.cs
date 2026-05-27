using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    // Запрос на список прикрепленных файлов с типом Event_Files
    public class ListEventFilesQuery : IRequest<OperationResult<PagedResult<Event_Files>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}