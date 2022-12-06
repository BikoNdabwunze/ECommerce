using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interface;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ILogger<ProductsProvider> logger;
        private readonly ProductsDbContext dbContext;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.dbContext = dbContext;

            SeedData();

        }


        private void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20,Inventory=100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 100 });
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool isSuccess, IEnumerable<Models.Product> Products, string ErrorMessages)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                
                if(products !=null && products.Any())
                {
                 var result=   mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);

                    return (true, result, null);
                }
                return (false, null,"Not Found");
            }
            catch (Exception e)
            {

                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
           
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null )
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);

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
    }
}
