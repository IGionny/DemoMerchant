using DemoMerchant.Sdk.Domain;
using Microsoft.AspNetCore.Mvc;
using ProductServices;

namespace DemoMerchant.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController] //see: https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#apicontroller-attribute
public class ProductCategoryController : BaseCrudApiController<ProductCategory, IProductCategoryService>
{
    public ProductCategoryController(IProductCategoryService productCategoryService) : base(productCategoryService)
    {
    }
}