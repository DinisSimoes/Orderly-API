using Orderly.Domain.Interfaces.Repositories.Customer;

namespace Orderly.Application.Services.Customer
{
    public class CustomerCommandService
    {
        private readonly ICustomerCommandRepository _customerCommandRepository;

        public CustomerCommandService(ICustomerCommandRepository customerCommandRepository)
        {
            _customerCommandRepository = customerCommandRepository;
        }

        public async Task AddAsync(Domain.Entities.Customer customer)
        {
            await _customerCommandRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(Domain.Entities.Customer customer)
        {
            await _customerCommandRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _customerCommandRepository.DeleteAsync(id);
        }
    }
}
