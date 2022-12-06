using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interface
{
    public interface IOrdersProvider
    {

        Task<(bool isSuccess, IEnumerable<Models.Order> orders, string ErrorMessages)> GetOrdersAsync(int customerId);
      
    }
}
