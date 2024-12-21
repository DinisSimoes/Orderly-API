using Microsoft.EntityFrameworkCore;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Product
{
    public class ProductQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetAllAsync()
        {
            return await _context.Products
                .ToListAsync();
        }
    }
}
