using Confluent.Kafka;
using Newtonsoft.Json;
using Orderly.Domain.Entities;
using Orderly.Domain.Interfaces.Messaging.Kafka;

namespace Orderly.Infrastructure.Messaging.Kafka.Producers
{
    public class OrderEventProducer : IOrderEventProducer
    {
        private readonly IProducer<string, string> _producer;

        public OrderEventProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishOrderCreatedEvent(string operation, Order order)
        {
            try
            {
                var orderEvent = new OrderEvent
                {
                    Id = order.Id,
                    Operation = operation,
                    Order = order
                };

                var message = new Message<string, string>
                {
                    Key = order.Id.ToString(),
                    Value = JsonConvert.SerializeObject(orderEvent)
                };

                await _producer.ProduceAsync("orders", message);

            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"Kafka produce failed. ErrorCode: {ex.Error.Code}, Reason: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

    }
}
