namespace Orderly.Domain.Interfaces.Services.OrderITem
{
    public interface IOrderItemCommandService
    {
        Task AddAsync(Entities.OrderItem orderItem);
        Task UpdateAsync(Entities.OrderItem orderItem);
        Task DeleteAsync(Guid id);
    }
}
