using Confluent.Kafka;
using MongoDB.Driver;
using Newtonsoft.Json;
using Orderly.Domain.Entities;

namespace Orderly.Infrastructure.Messaging.Kafka.Consumers
{
    public class OrderEventConsumer
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IConsumer<string, string> _consumer;

        public OrderEventConsumer(IMongoCollection<Order> orderCollection, IConsumer<string, string> consumer)
        {
            _orderCollection = orderCollection;
            _consumer = consumer;
        }

        public void StartConsuming()
        {
            _consumer.Subscribe("orders");

            while (true)
            {
                var consumeResult = _consumer.Consume(CancellationToken.None);
                var orderEvent = JsonConvert.DeserializeObject<Order>(consumeResult.Message.Value);

                // Verifica se o pedido já existe no MongoDB
                var existingOrder = _orderCollection.Find(o => o.Id == orderEvent.Id).FirstOrDefault();

                if (existingOrder != null)
                {
                    // Atualiza o pedido existente
                    _orderCollection.ReplaceOne(o => o.Id == orderEvent.Id, orderEvent);
                }
                else
                {
                    // Adiciona o novo pedido
                    _orderCollection.InsertOne(orderEvent);
                }
            }
        }
    }
}
