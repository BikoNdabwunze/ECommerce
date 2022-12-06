using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interface;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
       private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();

        }

        public async Task<(bool isSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {

           
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);

                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);

                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception e)
            {

                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Vernon",Address="123 Deez Street" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Maxwell", Address = "23 Nuts  avenue" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Clifford", Address = "73 TopG boul" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Greg", Address = "912 Deez Street" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessages)> GetCustomersAsync()
        {
            try
            {
                IEnumerable<Db.Customer> customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    IEnumerable<Models.Customer> result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
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
