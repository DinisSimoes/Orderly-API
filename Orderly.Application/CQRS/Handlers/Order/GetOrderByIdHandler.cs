using MediatR;
using Orderly.Application.CQRS.Queries.Order;
using Orderly.Application.CQRS.Responses.Order;
using Orderly.Application.DTOs;
using Orderly.Application.Services.Order;
using Orderly.Domain.Interfaces.Services.Order;

namespace Orderly.Application.CQRS.Handlers.Order
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderQueryService _orderQueryService;

        public GetOrderByIdHandler(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryService.GetByIdAsync(request.Id);
            if (order == null) return null;

            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.ProductName,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }
    }
}
