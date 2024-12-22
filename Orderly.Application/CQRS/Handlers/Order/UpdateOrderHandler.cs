using MediatR;
using Orderly.Application.CQRS.Commands.Order;
using Orderly.Domain.Interfaces.Services.Order;

namespace Orderly.Application.CQRS.Handlers.Order
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly IOrderCommandService _orderCommandService;

        public UpdateOrderHandler(IOrderQueryService orderQueryService, IOrderCommandService orderCommandService)
        {
            _orderQueryService = orderQueryService;
            _orderCommandService = orderCommandService;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryService.GetByIdAsync(request.Id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with Id {request.Id} not found.");
            }

            order.Status = request.Status;

            // Atualizar o pedido no banco de dados
            await _orderCommandService.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
