using Microsoft.EntityFrameworkCore;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.OrderItem
{
    public class OrderItemQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.OrderItem> GetByIdAsync(Guid id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.ProductId)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<Domain.Entities.OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.ProductId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Entities.OrderItem>> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.ProductId)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }
    }
}
