using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    // Моя команда для удаления записи о регистрации
    public class DeleteRegistrationCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}