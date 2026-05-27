using System.Threading.Tasks;
using KooliProjekt.Application.Features.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class EventsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Вывод списка мероприятий
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListEventsQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        // Получение одного мероприятия
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var query = new GetEventQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        // Создание или обновление мероприятия
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SaveEventCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }

        // Добавляю метод для удаления мероприятия, отправляя команду через Mediator
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteEventCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}