namespace Orderly.Domain.Interfaces.Services.Order
{
    public interface IOrderQueryService
    {
        Task<Domain.Entities.Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entities.Order>> GetAllAsync();
        Task<IEnumerable<Domain.Entities.Order>> GetByCustomerIdAsync(Guid customerId);
    }
}
