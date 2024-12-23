using Orderly.Domain.Entities;

namespace Orderly.Domain.Interfaces.Repositories.OrderItem
{
    public interface IOrderItemCommandRepository
    {
        Task AddAsync(Entities.OrderItem orderItem);
        Task UpdateAsync(Entities.OrderItem orderItem);
        Task DeleteAsync(Guid id);
    }
}
