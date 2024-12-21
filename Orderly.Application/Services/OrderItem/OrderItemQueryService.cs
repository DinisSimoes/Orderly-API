using Orderly.Domain.Interfaces.Repositories.OrderItem;
using Orderly.Domain.Interfaces.Services.OrderITem;

namespace Orderly.Application.Services.OrderItem
{
    public class OrderItemQueryService : IOrderItemQueryService
    {
        private readonly IOrderItemQueryRepository _orderItemQueryRepository;

        public OrderItemQueryService(IOrderItemQueryRepository orderItemQueryRepository)
        {
            _orderItemQueryRepository = orderItemQueryRepository;
        }

        public async Task<Domain.Entities.OrderItem> GetByIdAsync(Guid id)
        {
            return await _orderItemQueryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.OrderItem>> GetAllAsync()
        {
            return await _orderItemQueryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Domain.Entities.OrderItem>> GetByOrderIdAsync(Guid orderId)
        {
            return await _orderItemQueryRepository.GetByOrderIdAsync(orderId);
        }
    }
}
