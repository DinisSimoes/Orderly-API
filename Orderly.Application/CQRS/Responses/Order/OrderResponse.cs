using Orderly.Application.DTOs;
using Orderly.Domain.Entities;

namespace Orderly.Application.CQRS.Responses.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
