using System.Threading.Tasks;
using KooliProjekt.Application.Features.Registrations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class RegistrationsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListRegistrationsQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SaveRegistrationCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }

        // Добавляю метод для удаления регистрации
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteRegistrationCommand command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }
    }
}