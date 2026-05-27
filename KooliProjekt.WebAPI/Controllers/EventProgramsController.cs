using KooliProjekt.Application.Features.EventPrograms;
using KooliProjekt.Application.Features.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class EventProgramsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EventProgramsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListEventProgramsQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SaveEvent_ProgramCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}