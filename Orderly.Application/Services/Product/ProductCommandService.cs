using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Domain.Interfaces.Services.Product;

namespace Orderly.Application.Services.Product
{
    public class ProductCommandService : IProductCommandService
    {
        private readonly IProductCommandRepository _productCommandRepository;

        public ProductCommandService(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
        }

        public async Task AddAsync(Domain.Entities.Product product)
        {
            await _productCommandRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Domain.Entities.Product product)
        {
            await _productCommandRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productCommandRepository.DeleteAsync(id);
        }
    }
}
