using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Order
{
    public class OrderQueryRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public OrderQueryRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<Domain.Entities.Order> GetByIdAsync(Guid id)
        {
            return await _mongoDbContext.Orders
            .Find(o => o.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetAllAsync()
        {
            return await _mongoDbContext.Orders
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _mongoDbContext.Orders
                .Find(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
