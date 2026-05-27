using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    // 28.11
    // Kasutab IToDoListRepositoryt
    public class SaveToDoListCommandHandler : IRequestHandler<SaveToDoListCommand, OperationResult>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public SaveToDoListCommandHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<OperationResult> Handle(SaveToDoListCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var list = new ToDoList();
            if(request.Id != 0)
            {
                list = await _toDoListRepository.GetByIdAsync(request.Id);
            }

            list.Title = request.Title;

            await _toDoListRepository.SaveAsync(list);

            return result;
        }
    }
}
