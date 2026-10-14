#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories; // Используем репозиторий
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    public class SaveRegistrationCommandHandler : IRequestHandler<SaveRegistrationCommand, OperationResult>
    {
        private readonly IRegistrationRepository _repository;

        public SaveRegistrationCommandHandler(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveRegistrationCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var isNew = request.Id == 0;

            // Используем репозиторий для получения сущности
            var entity = isNew ? new Registration() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("Registration", "Регистрация не найдена");
                return result;
            }

            // Назначаем поля
            entity.UserId = request.UserId;
            entity.EventId = request.EventId;
            entity.Status = request.Status;
            entity.RegisteredAt = request.RegisteredAt == default ? DateTime.UtcNow : request.RegisteredAt;

            // Сохраняем через репозиторий
            await _repository.SaveAsync(entity);

            return result;
        }
    }
}
