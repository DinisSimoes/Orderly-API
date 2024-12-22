using MediatR;
using Orderly.Application.CQRS.Commands.Order;
using Orderly.Application.Services.Order;
using Orderly.Domain.Interfaces.Services.Order;

namespace Orderly.Application.CQRS.Handlers.Order
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderCommandService _orderCommandService;

        public DeleteOrderHandler(IOrderCommandService orderCommandService)
        {
            _orderCommandService = orderCommandService;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderCommandService.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
