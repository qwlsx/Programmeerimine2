using System.Threading.Tasks;
using KooliProjekt.Application.Features.EventFiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    // Наследую от базового класса, чтобы использовать метод Result()
    public class EventFilesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EventFilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListEventFilesQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SaveEvent_FileCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}