namespace Orderly.Domain.Interfaces.Services.Customer
{
    public interface ICustomerQueryService
    {
        Task<Entities.Customer> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Customer>> GetAllAsync();
        Task<Entities.Customer> GetByEmailAsync(string email);
    }
}
