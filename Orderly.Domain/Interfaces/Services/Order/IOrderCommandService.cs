namespace Orderly.Domain.Interfaces.Services.Order
{
    public interface IOrderCommandService
    {
        Task AddAsync(Domain.Entities.Order order);
        Task UpdateAsync(Domain.Entities.Order order);
        Task DeleteAsync(Guid id);
    }
}
