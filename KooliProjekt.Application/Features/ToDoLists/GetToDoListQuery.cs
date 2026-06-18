#nullable enable
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class GetToDoListQuery : IRequest<OperationResult<ToDoListDto?>>
    {
        public int Id { get; set; }
    }
}