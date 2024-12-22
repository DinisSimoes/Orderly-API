using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orderly.Application.CQRS.Commands.Order;
using Orderly.Application.CQRS.Queries.Order;

namespace Orderly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById([FromBody] GetOrderByIdQuery request)
        {
            var response = await _mediator.Send(request);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpGet("customer/{customerId:guid}")]
        public async Task<IActionResult> GetOrdersByCustomerId([FromBody] GetOrdersByCustomerIdQuery request)
        {
            var responses = await _mediator.Send(request);

            return Ok(responses);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromBody] GetAllOrdersQuery request)
        {
            var responses = await _mediator.Send(request);

            return Ok(responses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { Id = orderId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
