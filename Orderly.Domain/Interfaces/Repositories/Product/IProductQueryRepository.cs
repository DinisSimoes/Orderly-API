namespace Orderly.Domain.Interfaces.Repositories.Product
{
    public interface IProductQueryRepository
    {
        Task<Entities.Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Product>> GetAllAsync();
    }
}
