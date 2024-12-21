namespace Orderly.Domain.Interfaces.Services.OrderITem
{
    public interface IOrderItemQueryService
    {
        Task<Domain.Entities.OrderItem> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entities.OrderItem>> GetAllAsync();
        Task<IEnumerable<Domain.Entities.OrderItem>> GetByOrderIdAsync(Guid orderId);
    }
}
