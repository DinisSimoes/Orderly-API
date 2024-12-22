using Microsoft.EntityFrameworkCore;
using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Product
{
    public class ProductQueryRepository : IProductQueryRepository
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

        public async Task<Domain.Entities.Product> GetByNameAsync(string name)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
