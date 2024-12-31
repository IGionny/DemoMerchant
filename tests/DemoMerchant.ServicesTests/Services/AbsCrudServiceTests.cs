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
        using var context = new AppDbContext(Options);
        var service = CreateService(context);

        var entity = CreateEntity();

        // Act
        var createdEntity = await service.CreateAsync(entity);

        // Assert

        // Check if the entity has been created
        var foundEntity = await service.GetByIdAsync(createdEntity.Id.GetValueOrDefault());
        foundEntity.Should().NotBeNull();
        CreateAndGetAssertions(entity, foundEntity);
    }

    protected virtual void CreateAndGetAssertions(TEntity entity, TEntity foundEntity)
    {
        //Leave it empty
    }

    protected abstract TService CreateService(AppDbContext context);
    protected abstract TEntity CreateEntity();
}