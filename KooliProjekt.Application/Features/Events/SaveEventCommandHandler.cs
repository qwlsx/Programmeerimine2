#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories; // Добавляем namespace репозиториев
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    public class SaveEventCommandHandler : IRequestHandler<SaveEventCommand, OperationResult>
    {
        private readonly IEventRepository _repository; // Используем интерфейс репозитория

        public SaveEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveEventCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Если Id 0, создаем новый, иначе ищем существующий
            var entity = request.Id == 0
                ? new Event { CreatedAt = DateTime.UtcNow }
                : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Event", "Мероприятие не найдено");
                return result;
            }

            if (request.StartDateTime < entity.CreatedAt)
            {
                result.AddPropertyError("StartDateTime", "Дата мероприятия не может быть раньше даты создания");
                return result;
            }

            entity.Name = request.Name;
            entity.StartDateTime = request.StartDateTime;
            entity.Description = request.Description;
            entity.TotalSeats = request.TotalSeats;
            entity.Price = request.Price;

            // Сохраняем через репозиторий
            await _repository.SaveAsync(entity);

            return result;
        }
    }
}
