using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.ServicesTests.Services;

public abstract class AbsCrudServiceTests<TEntity, TService>
    where TEntity : AbsEntity where TService : IAbsService<TEntity>
{
    protected readonly DbContextOptions<AppDbContext> Options;

    protected AbsCrudServiceTests()
    {
        //Ensure unique database name per test class
        var name = "TestDb" + typeof(TEntity).Name;
        Options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: name)
            .Options;
    }

    [Fact]
    public virtual async Task CreateAndGet()
    {
        // Arrange
        await using var context = new AppDbContext(Options);
        var service = CreateService(context);

        var entity = CreateEntity();

        // Act
        var createdEntity = await service.CreateAsync(entity);

        // Assert

        // Check if the entity has been created
        var foundEntity = await service.GetByIdAsync(createdEntity.Id.GetValueOrDefault());
        foundEntity.Should().NotBeNull();
        CreateAndGetAssertions(entity, foundEntity!);
    }
    
    [Fact]
    public virtual async Task Delete()
    {
        // Arrange
        await using var context = new AppDbContext(Options);
        var service = CreateService(context);

        var entity = CreateEntity();
        var createdEntity = await service.CreateAsync(entity);
        createdEntity.Should().NotBeNull();
        createdEntity.Id.Should().NotBeEmpty();

        // Act
        var resultDelete = await service.DeleteAsync(createdEntity!.Id!.Value);
        
        // Assert
        resultDelete.Should().BeTrue();
        
        // Check if the entity has been deleted
        var foundEntity = await service.GetByIdAsync(createdEntity.Id.GetValueOrDefault());
        foundEntity.Should().BeNull();
    }
    
    [Fact]
    public virtual async Task Update()
    {
        // Arrange
        await using var context = new AppDbContext(Options);
        var service = CreateService(context);

        var entity = CreateEntity();
        var createdEntity = await service.CreateAsync(entity);
        createdEntity.Should().NotBeNull();
        createdEntity.Id.Should().NotBeEmpty();
        UpdateEntity(createdEntity);

        // Act
        await service.UpdateAsync(createdEntity);
        
        // Assert
        // Check if the entity has been changed
        var foundEntity = await service.GetByIdAsync(createdEntity.Id.GetValueOrDefault());
        AssertionsAfterUpdate(entity, foundEntity);
    }
    
    [Fact]
    public virtual async Task GetAll()
    {
        // Arrange
        await using var context = new AppDbContext(Options);
        var service = CreateService(context);

        var entity = CreateEntity();
        var createdEntity = await service.CreateAsync(entity);
        createdEntity.Should().NotBeNull();
        createdEntity.Id.Should().NotBeEmpty();

        // Act
        var all = service.GetAllAsync();
        
        // Assert
        var found = false;
        await foreach (var persisted in all)
        {
           if (persisted.Id == createdEntity.Id)
           {
               found = true;
               break;
           }
        }
        found.Should().BeTrue();
    }
    
    protected abstract void UpdateEntity(TEntity entity);
    protected abstract void AssertionsAfterUpdate(TEntity entity, TEntity foundEntity);

    protected virtual void CreateAndGetAssertions(TEntity entity, TEntity foundEntity)
    {
        //Leave it empty
    }

    protected abstract TService CreateService(AppDbContext context);
    protected abstract TEntity CreateEntity();
}