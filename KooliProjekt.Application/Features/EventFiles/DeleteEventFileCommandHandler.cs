using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class DeleteEventFileCommandHandler : IRequestHandler<DeleteEventFileCommand, OperationResult>
    {
        private readonly IEventFileRepository _repository;
        public DeleteEventFileCommandHandler(IEventFileRepository repository) => _repository = repository;

        public async Task<OperationResult> Handle(DeleteEventFileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("EventFile", "Файл не найден");
            await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
