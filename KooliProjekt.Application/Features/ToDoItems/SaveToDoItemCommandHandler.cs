#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class SaveToDoItemCommandHandler : IRequestHandler<SaveToDoItemCommand, OperationResult>
    {
        private readonly IToDoItemRepository _repository;

        public SaveToDoItemCommandHandler(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveToDoItemCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = request.Id == 0 ? new ToDoItem() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("ToDoItem", "Задача не найдена");
                return result;
            }

            entity.Title = request.Title;
            entity.IsDone = request.IsDone;
            entity.ToDoListId = request.ToDoListId;

            await _repository.SaveAsync(entity);
            return result;
        }
    }
}