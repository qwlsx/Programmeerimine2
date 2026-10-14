using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    // Хендлер для удаления пользователя из базы данных
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, OperationResult>
    {
        private readonly KooliProjekt.Application.Data.Repositories.IUserRepository _repository;

        public DeleteUserCommandHandler(KooliProjekt.Application.Data.Repositories.IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return result.AddPropertyError("User", "Пользователь не найден");
            await _repository.DeleteAsync(entity);

            return result;
        }
    }
}
