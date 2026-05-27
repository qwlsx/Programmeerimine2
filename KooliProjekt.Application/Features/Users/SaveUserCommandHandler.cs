#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories; // Добавляем репозиторий
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, OperationResult>
    {
        private readonly IUserRepository _repository;

        public SaveUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Получаем существующего или создаем нового
            var entity = request.Id == 0 ? new User() : await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddPropertyError("User", "Пользователь не найден");
                return result;
            }

            // Обновляем поля
            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Role = request.Role;
            entity.CreatedAt = request.CreatedAt;

            // Сохраняем через репозиторий
            await _repository.SaveAsync(entity);

            return result;
        }
    }
}