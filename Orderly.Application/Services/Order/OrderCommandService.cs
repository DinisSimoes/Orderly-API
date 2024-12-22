using Orderly.Domain.Entities;
using Orderly.Domain.Interfaces.Messaging.Kafka;
using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Domain.Interfaces.Services.Order;
using Orderly.Infrastructure.Messaging.Kafka.Producers;

namespace Orderly.Application.Services.Order
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IOrderEventProducer _orderEventProducer;

        public OrderCommandService(IOrderCommandRepository orderCommandRepository, IOrderQueryRepository orderQueryRepository, IOrderEventProducer orderEventProducer)
        {
            _orderCommandRepository = orderCommandRepository;
            _orderEventProducer = orderEventProducer;
            _orderQueryRepository = orderQueryRepository;
        }

        public async Task AddAsync(Domain.Entities.Order order)
        {
            await _orderCommandRepository.AddAsync(order);
            await _orderEventProducer.PublishOrderCreatedEvent("Create", order);
        }

        public async Task UpdateAsync(Domain.Entities.Order order)
        {
            await _orderCommandRepository.UpdateAsync(order);
            await _orderEventProducer.PublishOrderCreatedEvent("Update", order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderQueryRepository.GetByIdAsync(id);

            if (order != null)
            {
                await _orderCommandRepository.DeleteAsync(id);
                await _orderEventProducer.PublishOrderCreatedEvent("Delete", order);
            }
            else
            {
                throw new Exception($"Order with ID {id} not found.");
            }
        }
    }
}
