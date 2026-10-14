using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class GetEventFileQuery : IRequest<OperationResult<EventFileDto>>
    {
        public int Id { get; set; }
    }
}
