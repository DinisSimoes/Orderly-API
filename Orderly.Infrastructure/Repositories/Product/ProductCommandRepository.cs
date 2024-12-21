using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Product
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
