using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class ListToDoListsQuery : IRequest<OperationResult<PagedResult<ToDoList>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
