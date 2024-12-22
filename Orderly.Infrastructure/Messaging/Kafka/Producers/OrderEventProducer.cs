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

        public async Task PublishOrderCreatedEvent(Order order)
        {
            try
            {
                var orderCreatedEvent = new
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status.ToString(),
                    OrderItems = order.OrderItems.Select(item => new
                    {
                        Id = item.Id,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    })
                };

                var message = new Message<string, string>
                {
                    Key = order.Id.ToString(),
                    Value = JsonConvert.SerializeObject(orderCreatedEvent)
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
