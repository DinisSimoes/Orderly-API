using Orderly.Domain.Interfaces.Repositories.OrderItem;
using Orderly.Domain.Interfaces.Services.OrderITem;

namespace Orderly.Application.Services.OrderItem
{
    public class OrderItemCommandService : IOrderItemCommandService
    {
        private readonly IOrderItemCommandRepository _orderItemCommandRepository;

        public OrderItemCommandService(IOrderItemCommandRepository orderItemCommandRepository)
        {
            _orderItemCommandRepository = orderItemCommandRepository;
        }

        public async Task AddAsync(Domain.Entities.OrderItem orderItem)
        {
            await _orderItemCommandRepository.AddAsync(orderItem);
        }

        public async Task UpdateAsync(Domain.Entities.OrderItem orderItem)
        {
            await _orderItemCommandRepository.UpdateAsync(orderItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _orderItemCommandRepository.DeleteAsync(id);
        }
    }
}
