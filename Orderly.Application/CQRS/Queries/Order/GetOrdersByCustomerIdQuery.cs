using MediatR;
using Orderly.Application.CQRS.Responses.Order;

namespace Orderly.Application.CQRS.Queries.Order
{
    public class GetOrdersByCustomerIdQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public Guid CustomerId { get; set; }
    }
}
