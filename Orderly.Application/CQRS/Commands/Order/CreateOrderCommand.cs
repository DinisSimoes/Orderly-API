using MediatR;
using Orderly.Application.CQRS.Responses.Order;
using Orderly.Application.DTOs;
using Orderly.Domain.Entities;

namespace Orderly.Application.CQRS.Commands.Order
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
