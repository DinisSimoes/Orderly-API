using Orderly.Domain.Entities;

namespace Orderly.Domain.Interfaces.Repositories.Order
{
    public interface IOrderCommandRepository
    {
        Task AddAsync(Entities.Order order);
        Task UpdateAsync(Entities.Order order);
        Task DeleteAsync(Guid id);
    }
}
