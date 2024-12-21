using Confluent.Kafka;
using Newtonsoft.Json;
using Orderly.Domain.Entities;
using Orderly.Domain.Interfaces.Messaging.Kafka;

namespace Orderly.Infrastructure.Messaging.Kafka.Producers
{
    public class OrderEventProducer : IOrderEventProducer
    {
        private readonly IProducer<string, string> _producer;

        public OrderEventProducer(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public async Task PublishOrderCreatedEvent(Order order)
        {
            var orderCreatedEvent = new
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            };

            var message = new Message<string, string>
            {
                Key = order.Id.ToString(),
                Value = JsonConvert.SerializeObject(orderCreatedEvent)
            };

            await _producer.ProduceAsync("orders", message);
        }
    }
}
