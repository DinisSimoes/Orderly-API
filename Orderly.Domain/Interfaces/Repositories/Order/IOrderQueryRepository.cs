using Orderly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Interfaces.Repositories.Order
{
    public interface IOrderQueryRepository
    {
        Task<Entities.Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Order>> GetAllAsync();
        Task<IEnumerable<Entities.Order>> GetByCustomerIdAsync(Guid customerId);
    }
}
