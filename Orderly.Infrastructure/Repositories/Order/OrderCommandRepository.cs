using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Order
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
