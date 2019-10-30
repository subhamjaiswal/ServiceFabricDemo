using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ECommerceProductCatalog
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ECommerceProductCatalog : StatefulService,IProductCatalogService
    {
         private IProductRepository _repo;
        public ECommerceProductCatalog(StatefulServiceContext context)
            : base(context)
        {
            // _repo = new ServiceFabricProductRepository(this.StateManager); 
        }

        public  async Task AddProductAsync(Product product)
        {
              await  _repo.AddProduct(product);
        }

       public async Task<Product[]> GetAllProductAsync()
        {
                return (await _repo.GetAllProducts()).ToArray();
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                 new ServiceReplicaListener(context => new FabricTransportServiceRemotingListener(context,this))
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            _repo = new ServiceFabricProductRepository(this.StateManager);
            var productp1 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Microsoft Surface Pro",
                Description = "Microsofts Notebook Computer",
                Price = 80000,
                Availbility = 10

            };

            var productp2 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Apple Macbook Pro",
                Description = "Apples Profession Laptop",
                Price = 150000,
                Availbility = 20
            };

            var productp3 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Apple Air Pods",
                Description = "Apples Wirless Bluetooth Headset",
                Price = 15000,
                Availbility = 30
            };

          
            
            

            IEnumerable<Product> all = await _repo.GetAllProducts();

        }
    }
}
