using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using FluentAssertions;
using ProductServices;

namespace DemoMerchant.ServicesTests.Services;

public class ProductCategoryServiceTests : AbsCrudServiceTests<ProductCategory, ProductCategoryService>
{
    protected override ProductCategoryService CreateService(AppDbContext context)
    {
        return new ProductCategoryService(context);
    }

    protected override void UpdateEntity(ProductCategory entity)
    {
        entity.Name = "Really good category";
    }

    protected override void AssertionsAfterUpdate(ProductCategory entity, ProductCategory foundEntity)
    {
        foundEntity.Name.Should().Be("Really good category");
    }

    protected override void CreateAndGetAssertions(ProductCategory entity, ProductCategory foundEntity)
    {
        foundEntity.Name.Should().Be(entity.Name);
    }

    protected override ProductCategory CreateEntity()
    {
        var productCategory = DataMother.CreateProductCategory();
        productCategory.Id = null;
        return productCategory;
    }
}