using DemoMerchant.Sdk.Domain;
using Microsoft.AspNetCore.Mvc;
using OrderServices;

namespace DemoMerchant.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController] //see: https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#apicontroller-attribute
public class OrderApiController : BaseCrudApiController<Order, IOrderService>
{
    public OrderApiController(IOrderService orderService) : base(orderService)
    {
    }
}