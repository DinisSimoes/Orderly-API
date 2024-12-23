using Orderly.Domain.Interfaces.Repositories.Customer;

namespace Orderly.Application.Services.Customer
{
    public class CustomerQueryService
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public CustomerQueryService(ICustomerQueryRepository customerQueryRepository)
        {
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<Domain.Entities.Customer> GetByIdAsync(Guid id)
        {
            return await _customerQueryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Customer>> GetAllAsync()
        {
            return await _customerQueryRepository.GetAllAsync();
        }

        public async Task<Domain.Entities.Customer> GetByEmailAsync(string email)
        {
            return await _customerQueryRepository.GetByEmailAsync(email);
        }
    }
}
