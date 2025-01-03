﻿using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using DemoMerchant.Sdk.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomerServices;

public interface ICustomerService : IAbsService<Customer>
{
}

public class CustomerService : AbsService<Customer>, ICustomerService
{
    public CustomerService(AppDbContext context) : base(context)
    {
    }
}