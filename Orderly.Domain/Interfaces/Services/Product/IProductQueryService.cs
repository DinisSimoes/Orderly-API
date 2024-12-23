namespace Orderly.Domain.Interfaces.Services.Product
{
    public interface IProductQueryService
    {
        Task<Entities.Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Product>> GetAllAsync();
    }
}
