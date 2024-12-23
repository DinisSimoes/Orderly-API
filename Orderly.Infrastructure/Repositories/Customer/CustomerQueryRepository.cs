using Microsoft.EntityFrameworkCore;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Customer
{
    public class CustomerQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Orders) // Carregar as ordens associadas
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Domain.Entities.Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Orders) // Carregar as ordens associadas
                .ToListAsync();
        }

        public async Task<Domain.Entities.Customer> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .Include(c => c.Orders) // Carregar as ordens associadas
                .FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
