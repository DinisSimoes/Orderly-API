using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Domain.Interfaces.Services.Order;

namespace Orderly.Application.Services.Order
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IOrderQueryRepository _orderQueryRepository;

        public OrderQueryService(IOrderQueryRepository orderQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository;
        }

        public async Task<Domain.Entities.Order> GetByIdAsync(Guid id)
        {
            return await _orderQueryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetAllAsync()
        {
            return await _orderQueryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _orderQueryRepository.GetByCustomerIdAsync(customerId);
        }
    }
}
