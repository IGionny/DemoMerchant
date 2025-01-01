using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DemoMerchant.WebApi.ApiControllers;


/// <summary>
/// A basic abstract class to handle CRUD operations for an entity
/// </summary>
public abstract class BaseCrudApiController<TEntity, TService> : ControllerBase
    where TEntity : AbsEntity 
    where TService : IAbsService<TEntity>
{
    protected readonly TService _service;

    public BaseCrudApiController(TService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    
    /// <summary>
    /// Fetch all items
    /// </summary>
    [HttpGet("All")]
    public virtual async IAsyncEnumerable<TEntity> GetAllAsync()
    {
        var items = _service.GetAllAsync();
        await foreach (var customer in items)
        {
            yield return customer;
        }
    }

    /// <summary>
    /// Fetch an item by id
    /// </summary>
    /// <param name="id">The identifier</param>
    [HttpGet("{id:guid}")]
    public virtual async Task<Results<NotFound, Ok<TEntity>>> GetAsync(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item != null)
        {
            return TypedResults.Ok(item);
        }

        return TypedResults.NotFound();
    }

    /// <summary>
    /// Create a new item
    /// </summary>
    [HttpPost]
    public virtual async Task<Results<BadRequest, Created<TEntity>>> CreateAsync(TEntity item)
    {
        var createdCustomer = await _service.CreateAsync(item);
        var location = Url.Action(nameof(CreateAsync), new { id = createdCustomer.Id }) ?? $"/{createdCustomer.Id}";
        return TypedResults.Created(location, createdCustomer);
    }

    /// <summary>
    /// Update an item
    /// </summary>
    [HttpPut]
    public virtual async Task<Results<NotFound, Ok<TEntity>>> UpdateAsync(TEntity item)
    {
        await _service.UpdateAsync(item);
        var reread = await _service.GetByIdAsync(item.Id.GetValueOrDefault());
        return TypedResults.Ok(reread);
    }

    /// <summary>
    /// Delete an item
    /// </summary>
    [HttpDelete("{id:guid}")]
    public virtual async Task<Results<NotFound, Ok>> DeleteAsync(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
        {
            return TypedResults.NotFound();
        }

        await _service.DeleteAsync(id);
        return TypedResults.Ok();
    }
    
}