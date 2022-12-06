using ECommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interface
{
  public  interface IOrdersService
    {
      Task<(bool IsSucces , IEnumerable<Order> Orders, string ErrorMessage )>  
            GetOrdersAsync(int customerId);
    }
}
