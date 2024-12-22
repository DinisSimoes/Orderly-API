using MediatR;
using Orderly.Application.CQRS.Queries.Order;
using Orderly.Application.CQRS.Responses.Order;
using Orderly.Application.DTOs;
using Orderly.Application.Services.Order;
using Orderly.Domain.Interfaces.Services.Order;

namespace Orderly.Application.CQRS.Handlers.Order
{
    public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderQueryService _orderQueryService;

        public GetOrdersByCustomerIdHandler(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderQueryService.GetByCustomerIdAsync(request.CustomerId);

            return orders.Select(order => new OrderResponse
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
            });
        }
    }
}
