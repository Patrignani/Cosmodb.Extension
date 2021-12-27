using Cosmodb.Extension.Core.Options;
using Cosmodb.Extension.Core.Utils;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmodb.Extension.Core.Builder
{
    public class CosmosDbBuilder
    {
        private readonly CosmosClient _cosmosClient;
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly CosmosClientExtension _cosmosClientExtension;
        private Semaphore _semaphore => new Semaphore(0, 1);


        public CosmosDbBuilder(IServiceCollection serviceDescriptors, IConfiguration configuration, CosmosClient cosmosClient, CosmosClientExtension cosmosClientExtension)
        {
            _cosmosClient = cosmosClient;
            _services = serviceDescriptors;
            _configuration = configuration;
            _cosmosClientExtension = cosmosClientExtension;
        }

        public CosmosDbBuilder AddDatabase()
        {
            Task.Run(async () =>
            {
                DatabaseResponse databaseResponse = await _cosmosClient.CreateDatabaseAsync("aaa", 10000);

                _semaphore.Release();
            });

            _semaphore.WaitOne();
            return this;
        }

        public void Build()
        {
            _services.AddIoc(Enums.RegisterServiceType.Singleton, x => _cosmosClientExtension);
            _services.AddIoc(_cosmosClientExtension.RegisterServiceType, x => _cosmosClient);
        }

    }
}
