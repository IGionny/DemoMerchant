using DemoMerchant.Sdk.Domain;
using Microsoft.AspNetCore.Mvc;
using OrderServices;

namespace DemoMerchant.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController] //see: https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#apicontroller-attribute
public class OrderController : BaseCrudApiController<Order, IOrderService>
{
    public OrderController(IOrderService orderService) : base(orderService)
    {
    }
}