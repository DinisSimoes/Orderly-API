using Orderly.Domain.Interfaces.Repositories.OrderItem;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.OrderItem
{
    public class OrderItemCommandRepository : IOrderItemCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
