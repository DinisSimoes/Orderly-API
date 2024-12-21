using Orderly.Domain.Interfaces.Repositories.Customer;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Customer
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
