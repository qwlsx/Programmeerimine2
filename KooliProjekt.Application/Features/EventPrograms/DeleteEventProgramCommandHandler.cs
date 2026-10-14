using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    public class DeleteEventProgramCommandHandler : IRequestHandler<DeleteEventProgramCommand, OperationResult>
    {
        private readonly IEventProgramRepository _repository;
        public DeleteEventProgramCommandHandler(IEventProgramRepository repository) => _repository = repository;

        public async Task<OperationResult> Handle(DeleteEventProgramCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("EventProgram", "Пункт программы не найден");
            await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
