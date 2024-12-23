using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Domain.Interfaces.Services.Product;

namespace Orderly.Application.Services.Product
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public ProductQueryService(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task<Domain.Entities.Product> GetByIdAsync(Guid id)
        {
            return await _productQueryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetAllAsync()
        {
            return await _productQueryRepository.GetAllAsync();
        }
    }
}
