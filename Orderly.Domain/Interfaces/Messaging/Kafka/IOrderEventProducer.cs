using Orderly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Interfaces.Messaging.Kafka
{
    public interface IOrderEventProducer
    {
        Task PublishOrderCreatedEvent(string operation, Order order);
    }
}
