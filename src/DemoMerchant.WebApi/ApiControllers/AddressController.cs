using AddressServices;
using DemoMerchant.Sdk.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DemoMerchant.WebApi.ApiControllers;

[Route("api/[Controller]")]
[ApiController] //see: https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#apicontroller-attribute
public class AddressController : BaseCrudApiController<Address, IAddressService>
{
    public AddressController(IAddressService addressService) : base(addressService)
    {
    }
}