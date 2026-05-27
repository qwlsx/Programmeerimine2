using System.Threading.Tasks;
using KooliProjekt.Application.Features.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class InvoicesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListInvoicesQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SaveInvoiceCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }

        // Добавляю метод для удаления счета, отправляя команду через Mediator
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteInvoiceCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}