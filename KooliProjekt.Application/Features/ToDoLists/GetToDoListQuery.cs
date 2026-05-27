using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class GetToDoListQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
