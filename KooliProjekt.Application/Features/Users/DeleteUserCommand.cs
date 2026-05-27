using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    // Моя команда для удаления пользователя
    public class DeleteUserCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}