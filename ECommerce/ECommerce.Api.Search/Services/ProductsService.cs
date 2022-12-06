using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly ILogger<IProductsService> logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<IProductsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {

                var client = httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/products");
              if(response.IsSuccessStatusCode)  {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);

            }
            catch (Exception e)
            {

                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
