namespace Orderly.Domain.Interfaces.Repositories.Customer
{
    public interface ICustomerCommandRepository
    {
        Task AddAsync(Entities.Customer customer);
        Task UpdateAsync(Entities.Customer customer);
        Task DeleteAsync(Guid id);
    }
}
