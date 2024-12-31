using DemoMerchant.Sdk.Domain;

namespace DemoMerchant.Sdk.Services;

public interface IAbsService<T> where T: AbsEntity
{
    Task<T> CreateAsync(T item);
    Task<T?> GetByIdAsync(Guid id);
    IAsyncEnumerable<T> GetAllAsync();
    Task UpdateAsync(T item);
    Task DeleteAsync(Guid id);
}