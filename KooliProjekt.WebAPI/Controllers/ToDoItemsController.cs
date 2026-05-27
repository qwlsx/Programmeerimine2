using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.ToDoItems;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetToDoItemQuery { Id = id });

            if (result.HasErrors)
            {
                return BadRequest(result);
            }

            return result.Value == null ? NotFound() : Ok(result.Value);
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveToDoItemCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.HasErrors)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}