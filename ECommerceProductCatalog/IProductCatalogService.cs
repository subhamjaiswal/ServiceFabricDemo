using ECommerce.Domain;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProductCatalog
{
    public interface IProductCatalogService:IService
    {
        Task<Product[]> GetAllProductAsync();
        Task AddProductAsync(Product product);
    }
}
