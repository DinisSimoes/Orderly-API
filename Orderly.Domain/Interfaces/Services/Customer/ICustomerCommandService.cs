namespace Orderly.Domain.Interfaces.Services.Customer
{
    public interface ICustomerCommandService
    {
        Task AddAsync(Entities.Customer customer);
        Task UpdateAsync(Entities.Customer customer);
        Task DeleteAsync(Guid id);
    }
}
