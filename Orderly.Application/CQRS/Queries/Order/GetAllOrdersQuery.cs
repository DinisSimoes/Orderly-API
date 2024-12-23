using MediatR;
using Orderly.Application.CQRS.Responses.Order;

namespace Orderly.Application.CQRS.Queries.Order
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderResponse>>
    {
    }
}
