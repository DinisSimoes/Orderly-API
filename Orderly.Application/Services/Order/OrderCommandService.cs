using Orderly.Domain.Interfaces.Messaging.Kafka;
using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Domain.Interfaces.Services.Order;
using Orderly.Infrastructure.Messaging.Kafka.Producers;

namespace Orderly.Application.Services.Order
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly IOrderEventProducer _orderEventProducer;

        public OrderCommandService(IOrderCommandRepository orderCommandRepository, IOrderEventProducer orderEventProducer)
        {
            _orderCommandRepository = orderCommandRepository;
            _orderEventProducer = orderEventProducer;
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
