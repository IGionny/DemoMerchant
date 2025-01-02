using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using FluentAssertions;
using ProductServices;

namespace DemoMerchant.ServicesTests.Services;

public class ProductServiceTests : AbsCrudServiceTests<Product, ProductService>
{
    protected override ProductService CreateService(AppDbContext context)
    {
        return new ProductService(context);
    }

    protected override void CreateAndGetAssertions(Product entity, Product foundEntity)
    {
        foundEntity.Name.Should().Be(entity.Name);
        foundEntity.Category.Should().NotBeNull();
        foundEntity.Category!.Id.Should().Be(entity.Category!.Id);
    }

    protected override Product CreateEntity()
    {
        var product = DataMother.CreateProduct();
        product.Id = null;
        return product;
    }
}