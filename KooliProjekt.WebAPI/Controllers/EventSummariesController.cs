using System.Threading.Tasks;
using KooliProjekt.Application.Features.EventSummaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class EventSummariesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EventSummariesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListEventSummariesQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SaveEvent_SummaryCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}