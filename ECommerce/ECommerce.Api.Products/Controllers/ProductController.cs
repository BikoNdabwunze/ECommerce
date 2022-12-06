using ECommerce.Api.Products.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{

    [ApiController]
    [Route("api/products")]

    public class ProductController :ControllerBase
    {
        private readonly IProductsProvider productsProvider;
        public ProductController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;

        }

        [HttpGet]
       public async Task<IActionResult> GetProductsAsync()
        {
          var result= await  productsProvider.GetProductsAsync();
            if(result.isSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();

        }
    }
}
