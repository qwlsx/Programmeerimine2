using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    // Моя команда для удаления мероприятия по его ID
    public class DeleteEventCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}