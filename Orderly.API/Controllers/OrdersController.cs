using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orderly.Application.CQRS.Commands.Order;
using Orderly.Application.CQRS.Queries.Order;
using Orderly.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Obtém um pedido pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do pedido.</param>
        /// <returns>Os detalhes do pedido.</returns>
        /// <response code="200">Retorna o pedido.</response>
        /// <response code="404">Caso o pedido não seja encontrado.</response>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Obter pedido por ID", Description = "Recupera os detalhes do pedido com base no ID do pedido.")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var response = await _mediator.Send(new GetOrderByIdQuery{ Id = id});

            if (response == null) return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Obtém todos os pedidos de um cliente específico.
        /// </summary>
        /// <param name="customerId">O identificador único do cliente.</param>
        /// <returns>Uma lista de pedidos do cliente especificado.</returns>
        [HttpGet("customer/{customerId:guid}")]
        [SwaggerOperation(Summary = "Obter pedidos por ID de cliente", Description = "Recupera todos os pedidos associados a um cliente.")]
        public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId)
        {
            var responses = await _mediator.Send(new GetOrdersByCustomerIdQuery{ CustomerId = customerId });

            return Ok(responses);
        }

        /// <summary>
        /// Obtém uma lista de todos os pedidos.
        /// </summary>
        /// <returns>Uma lista de todos os pedidos.</returns>
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Obter todos os pedidos", Description = "Recupera uma lista de todos os pedidos.")]
        public async Task<IActionResult> GetAllOrders()
        {
            var responses = await _mediator.Send(new GetAllOrdersQuery());

            return Ok(responses);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="command">O comando contendo os detalhes do pedido a ser criado.</param>
        /// <returns>O ID do pedido criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Criar um novo pedido", Description = "Cria um novo pedido com base nos detalhes fornecidos.")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { Id = orderId });
        }

        /// <summary>
        /// Atualiza um pedido existente pelo seu ID.
        /// </summary>
        /// <param name="id">O identificador único do pedido a ser atualizado.</param>
        /// <param name="command">O comando contendo os novos detalhes do pedido.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(Summary = "Atualizar um pedido existente", Description = "Atualiza os detalhes de um pedido existente.")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deleta um pedido pelo seu ID.
        /// </summary>
        /// <param name="id">O identificador único do pedido a ser deletado.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Deletar um pedido", Description = "Deleta um pedido pelo seu ID.")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
