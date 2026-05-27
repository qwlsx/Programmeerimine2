#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, OperationResult<ToDoItem?>>
    {
        private readonly IToDoItemRepository _repository;

        public GetToDoItemQueryHandler(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<ToDoItem?>> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ToDoItem?>();
            result.Value = await _repository.GetByIdAsync(request.Id);
            return result;
        }
    }
}