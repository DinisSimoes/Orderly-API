using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Infrastructure.Messaging.Kafka.Producers;

namespace Orderly.Application.Services.Order
{
    public class OrderCommandService
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly OrderEventProducer _orderEventProducer;

        public OrderCommandService(IOrderCommandRepository orderCommandRepository)
        {
            _orderCommandRepository = orderCommandRepository;
        }

        public async Task AddAsync(Domain.Entities.Order order)
        {
            await _orderCommandRepository.AddAsync(order);
            await _orderEventProducer.PublishOrderCreatedEvent(order);
        }

        public async Task UpdateAsync(Domain.Entities.Order order)
        {
            await _orderCommandRepository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _orderCommandRepository.DeleteAsync(id);
        }
    }
}
