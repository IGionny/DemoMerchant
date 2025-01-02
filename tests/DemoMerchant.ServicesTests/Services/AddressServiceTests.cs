using AddressServices;
using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;

namespace DemoMerchant.ServicesTests.Services;

public class AddressServiceTests : AbsCrudServiceTests<Address, AddressService>
{
    protected override AddressService CreateService(AppDbContext context)
    {
        return new AddressService(context);
    }

    protected override Address CreateEntity()
    {
        var address = DataMother.CreateAddress();
        address.Id = null;
        return address;
    }
}