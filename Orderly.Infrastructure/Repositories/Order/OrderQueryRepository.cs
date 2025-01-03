﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Infrastructure.Persistence;

namespace Orderly.Infrastructure.Repositories.Order
{
    public class OrderQueryRepository : IOrderQueryRepository
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
