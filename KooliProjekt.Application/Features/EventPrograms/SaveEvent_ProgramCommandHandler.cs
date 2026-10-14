#nullable enable
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    public class SaveEvent_ProgramCommandHandler : IRequestHandler<SaveEvent_ProgramCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveEvent_ProgramCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveEvent_ProgramCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var isNew = request.Id == 0;
            var entity = isNew ? new Event_Program() : await _dbContext.Event_Programs.FindAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Event_Program", "Пункт программы не найден");
                return result;
            }

            var eventEntity = await _dbContext.Events.FindAsync(new object[] { request.EventId }, cancellationToken);
            if (eventEntity == null)
            {
                result.AddPropertyError("EventId", "Мероприятие не найдено");
                return result;
            }
            if (request.Time < eventEntity.StartDateTime)
            {
                result.AddPropertyError("Time", "Время пункта программы не может быть раньше начала мероприятия");
                return result;
            }

            entity.EventId = request.EventId;
            entity.Time = request.Time;
            entity.Topic = request.Topic;

            if (isNew) await _dbContext.Event_Programs.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
