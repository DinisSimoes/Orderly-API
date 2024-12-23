using MediatR;

namespace Orderly.Application.CQRS.Commands.Order
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
