namespace Orderly.Domain.Interfaces.Repositories.Product
{
    public interface IProductCommandRepository
    {
        Task AddAsync(Entities.Product product);
        Task UpdateAsync(Entities.Product product);
        Task DeleteAsync(Guid id);
    }
}
