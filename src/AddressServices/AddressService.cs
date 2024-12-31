using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;

namespace AddressServices;

public interface IAddressService : IAbsService<Address>
{
}

public class AddressService : AbsService<Address>, IAddressService
{
    public AddressService(AppDbContext context) : base(context)
    {
    }
}