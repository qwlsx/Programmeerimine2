#nullable enable
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQuery : IRequest<OperationResult<ToDoItemDto?>>
    {
        public int Id { get; set; }
    }
}