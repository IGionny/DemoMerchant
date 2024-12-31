using CustomerServices;
using DemoMerchant.Sdk.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DemoMerchant.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController] //see: https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#apicontroller-attribute
public class AddressApiController : BaseCrudApiController<Address, IAddressService>
{
    public AddressApiController(IAddressService addressService) : base(addressService)
    {
    }
}