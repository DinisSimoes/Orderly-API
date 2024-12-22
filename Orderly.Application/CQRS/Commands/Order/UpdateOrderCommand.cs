using MediatR;
using Orderly.Application.DTOs;
using Orderly.Domain.Entities;
using System.Text.Json.Serialization;

namespace Orderly.Application.CQRS.Commands.Order
{
    public class UpdateOrderCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
