using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Model;
using ECommerce.Domain;
using ECommerceProductCatalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCatalogService _service;

        public ProductsController()
        {
            var proxyFactory = new ServiceProxyFactory(
               c => new FabricTransportServiceRemotingClientFactory());

            _service = proxyFactory.CreateServiceProxy<IProductCatalogService>(new Uri("fabric:/ECommerce/ECommerceProductCatalog"), new ServicePartitionKey(0));
        }
        [HttpGet]

    public async  Task<IEnumerable<ApiProduct>> GetAsync()
        {
            IEnumerable<Product> allproducts = await _service.GetAllProductAsync();

            var productToReturn = allproducts.Select(p => new ApiProduct()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsAvailable = p.Availbility > 0



            });

            return productToReturn;
        }

        [HttpPost("addproduct")]
        public async Task PostAsync([FromBody] ApiProduct product)
        {
          await  _service.AddProductAsync(
                new Product()
                {
                    
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Availbility = 1
                    
                }
                
                
                );

        }

       
    }
}
