using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class SaveEvent_FileCommandHandler : IRequestHandler<SaveEvent_FileCommand, OperationResult>
    {
        private readonly IEventFileRepository _repository;

        public SaveEvent_FileCommandHandler(IEventFileRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveEvent_FileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = request.Id == 0 ? new Event_Files() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Event_Files", "Файл не найден");
                return result;
            }

            entity.EventId = request.EventId;
            entity.FileUrl = request.FileUrl;
            entity.FileType = request.FileType;

            await _repository.SaveAsync(entity);
            return result;
        }
    }
}
