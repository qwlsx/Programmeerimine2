using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    public class DeleteEventProgramCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
