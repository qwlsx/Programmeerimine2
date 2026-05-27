#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQuery : IRequest<OperationResult<ToDoItem?>>
    {
        public int Id { get; set; }
    }
}