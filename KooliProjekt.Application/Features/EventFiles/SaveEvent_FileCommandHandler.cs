using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class SaveEvent_FileCommandHandler : IRequestHandler<SaveEvent_FileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveEvent_FileCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveEvent_FileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var entity = request.Id == 0 ? new Event_Files() : await _dbContext.Event_Files.FindAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Event_Files", "Файл не найден");
                return result;
            }

            entity.EventId = request.EventId;
            entity.FileUrl = request.FileUrl;
            entity.FileType = request.FileType;

            if (request.Id == 0)
            {
                _dbContext.Event_Files.Add(entity);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}