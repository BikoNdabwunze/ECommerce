using ECommerce.Api.Orders.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
