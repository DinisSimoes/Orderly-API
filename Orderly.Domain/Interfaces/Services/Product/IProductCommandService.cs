namespace Orderly.Domain.Interfaces.Services.Product
{
    public interface IProductCommandService
    {
        Task AddAsync(Entities.Product product);
        Task UpdateAsync(Entities.Product product);
        Task DeleteAsync(Guid id);
    }
}
