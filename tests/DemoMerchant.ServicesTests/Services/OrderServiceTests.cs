using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using FluentAssertions;
using OrderServices;

namespace DemoMerchant.ServicesTests.Services;

public class OrderServiceTests : AbsCrudServiceTests<Order, OrderService>
{
    protected override OrderService CreateService(AppDbContext context)
    {
        return new OrderService(context);
    }

    protected override void UpdateEntity(Order entity)
    {
        entity.OrderDate = new DateTime(2020,10,12,13,14,15, DateTimeKind.Utc);
    }

    protected override void AssertionsAfterUpdate(Order entity, Order foundEntity)
    {
        foundEntity.OrderDate.Should().Be(new DateTime(2020, 10, 12, 13, 14, 15, DateTimeKind.Utc));
    }

    protected override void CreateAndGetAssertions(Order entity, Order foundEntity)
    {
        foundEntity.Customer.Should().NotBeNull();
        foundEntity.Customer!.Id.Should().Be(entity.Customer!.Id);
        foundEntity.Items.Should().HaveCount(1);
        foundEntity.Items[0]!.Product!.Id.Should().Be(entity.Items[0]!.Product!.Id);
        foundEntity.Items[0].Quantity.Should().Be(entity.Items[0].Quantity);
        foundEntity.Items[0].Price.Should().Be(entity.Items[0].Price);
    }


    protected override Order CreateEntity()
    {
        var order = DataMother.CreateOrder();
        order.Id = null;
        return order;
    }
}