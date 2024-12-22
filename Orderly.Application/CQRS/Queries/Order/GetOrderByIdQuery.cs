using MediatR;
using Orderly.Application.CQRS.Responses.Order;

namespace Orderly.Application.CQRS.Queries.Order
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public Guid Id { get; set; }
    }
}
