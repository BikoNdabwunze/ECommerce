using ECommerce.Api.Orders.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController :ControllerBase
    {
        private readonly IOrdersProvider orderProvider;

        public OrderController(IOrdersProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }

       

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await orderProvider.GetOrdersAsync(customerId);
            if (result.isSuccess)
            {
                return Ok(result.orders);
            }
            return NotFound();

        }


    }
}
