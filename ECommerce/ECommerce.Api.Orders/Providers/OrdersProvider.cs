using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interface;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {

        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();

        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order() { Id = 1, CustomerId = 1, Date = DateTime.Now, Total = 158.99, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id=1, OrderId= 1, ProductId=1,Quantity=3,UnitPrice=32.67}, new Db.OrderItem { Id = 2, OrderId = 1, ProductId = 3, Quantity = 2, UnitPrice = 7.91 }, new Db.OrderItem { Id = 3, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 45.16 } } });
                dbContext.Orders.Add(new Db.Order() { Id = 2, CustomerId = 3, Date = DateTime.Now, Total = 201.99, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 4, OrderId = 2, ProductId = 4, Quantity = 13, UnitPrice = 2.79 }, new Db.OrderItem { Id = 5, OrderId = 2, ProductId = 3, Quantity = 4, UnitPrice = 7.91 }, new Db.OrderItem { Id = 6, OrderId = 1, ProductId = 2, Quantity = 6, UnitPrice = 22.34 } } });
                dbContext.Orders.Add(new Db.Order() { Id = 3, CustomerId = 2, Date = DateTime.Now, Total = 767.40, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 7, OrderId = 3, ProductId = 6, Quantity = 7, UnitPrice = 27.84 }, new Db.OrderItem { Id = 8, OrderId = 3, ProductId = 2, Quantity = 5, UnitPrice = 94.11 }, new Db.OrderItem { Id = 9, OrderId = 3, ProductId = 3, Quantity = 3, UnitPrice = 47.99 } } });
                dbContext.Orders.Add(new Db.Order() { Id = 4, CustomerId = 3, Date = DateTime.Now, Total = 158.99, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 10, OrderId = 4, ProductId = 1, Quantity = 3, UnitPrice = 32.67 }, new Db.OrderItem { Id = 11, OrderId = 1, ProductId = 3, Quantity = 2, UnitPrice = 7.91 }, new Db.OrderItem { Id = 12, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 45.16 } } });
                dbContext.Orders.Add(new Db.Order() { Id = 5, CustomerId = 2, Date = DateTime.Now, Total = 201.99, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 23, OrderId = 5, ProductId = 4, Quantity = 13, UnitPrice = 2.79 }, new Db.OrderItem { Id = 14, OrderId = 2, ProductId = 3, Quantity = 4, UnitPrice = 7.91 }, new Db.OrderItem { Id = 15, OrderId = 1, ProductId = 2, Quantity = 6, UnitPrice = 22.34 } } });
                dbContext.Orders.Add(new Db.Order() { Id = 6, CustomerId = 1, Date = DateTime.Now, Total = 767.40, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 26, OrderId = 6, ProductId = 4, Quantity = 7, UnitPrice = 27.84 }, new Db.OrderItem { Id = 17, OrderId = 3, ProductId = 1, Quantity = 5, UnitPrice = 94.11 }, new Db.OrderItem { Id = 18, OrderId = 3, ProductId = 1, Quantity = 3, UnitPrice = 47.99 } } });

                dbContext.SaveChanges();
            }
        }

       

        public  async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string ErrorMessages)> GetOrdersAsync(int customerId)
        {
            try
            {
                IEnumerable<Db.Order> orders = await dbContext.Orders.ToListAsync();
                orders = orders.Where(o => o.CustomerId == customerId).Select(o=>o).ToList();
                if (orders != null && orders.Any())
                {
                    IEnumerable<Models.Order> result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
