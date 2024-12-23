namespace Orderly.Domain.Interfaces.Repositories.Customer
{
    public interface ICustomerQueryRepository
    {
        Task<Entities.Customer> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Customer>> GetAllAsync();
        Task<Entities.Customer> GetByEmailAsync(string email);
    }
}
