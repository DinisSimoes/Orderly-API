using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Interfaces.Repositories.OrderItem
{
    public interface IOrderItemQueryRepository
    {
        Task<Entities.OrderItem> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.OrderItem>> GetAllAsync();
        Task<IEnumerable<Entities.OrderItem>> GetByOrderIdAsync(Guid orderId);
    }
}
