using DemoMerchant.Sdk.Domain;

namespace DemoMerchant.Sdk.Services;

public abstract class AbsService<T> : IAbsService<T> where T : AbsEntity
{
    protected readonly AppDbContext _context;

    public AbsService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual async Task<T> CreateAsync(T item)
    {
        if (item.Id.HasValue)
        {
            throw new ArgumentException("Item should not have an Id when creating", nameof(item));
        }
        item.CreatedAt = DateTime.UtcNow;
        _context.Set<T>().Attach(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual IAsyncEnumerable<T> GetAllAsync()
    {
        return _context.Set<T>().AsAsyncEnumerable();
    }

    public virtual async Task UpdateAsync(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item != null)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}