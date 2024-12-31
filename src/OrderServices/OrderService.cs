using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;

namespace OrderServices;

public interface IOrderService : IAbsService<Order>
{
}

public class OrderService : AbsService<Order>, IOrderService
{
    public OrderService(AppDbContext context) : base(context)
    {
    }
}