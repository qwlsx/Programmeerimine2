using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    // Мой конверт для получения одного мероприятия по его Id
    public class GetEventQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}