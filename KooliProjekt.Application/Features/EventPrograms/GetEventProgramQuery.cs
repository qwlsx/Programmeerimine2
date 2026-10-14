using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    public class GetEventProgramQuery : IRequest<OperationResult<EventProgramDto>>
    {
        public int Id { get; set; }
    }
}
