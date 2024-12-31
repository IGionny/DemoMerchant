using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;

namespace ProductServices;

public interface IProductService : IAbsService<Product>
{
}

public class ProductService : AbsService<Product>, IProductService
{
    public ProductService(AppDbContext context) : base(context)
    {
    }
}

