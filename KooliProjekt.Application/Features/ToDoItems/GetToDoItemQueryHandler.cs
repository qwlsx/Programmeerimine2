#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, OperationResult<ToDoItemDto?>>
    {
        private readonly IToDoItemRepository _repository;
        public GetToDoItemQueryHandler(IToDoItemRepository repository) => _repository = repository;

        public async Task<OperationResult<ToDoItemDto?>> Handle(GetToDoItemQuery request, CancellationToken ct)
        {
            var result = new OperationResult<ToDoItemDto?>();
            if (request == null) return result;

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity != null)
            {
                result.Value = new ToDoItemDto { Id = entity.Id, Title = entity.Title, IsDone = entity.IsDone };
            }
            return result;
        }
    }
}