﻿
using ECommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interface

{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessages)> GetCustomersAsync();
        Task<(bool isSuccess,Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
