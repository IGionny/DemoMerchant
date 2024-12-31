using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;

namespace ProductServices;

public interface IProductCategoryService : IAbsService<ProductCategory>
{
}

public class ProductCategoryService : AbsService<ProductCategory>, IProductCategoryService
{
    public ProductCategoryService(AppDbContext context) : base(context)
    {
    }
}