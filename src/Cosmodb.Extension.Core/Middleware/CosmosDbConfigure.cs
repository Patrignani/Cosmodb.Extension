using Cosmodb.Extension.Core.Builder;
using Cosmodb.Extension.Core.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cosmodb.Extension.Core.Middleware
{
    public static class CosmosDbConfigure
    {
        public static CosmosDbBuilder AddCosmos(this IServiceCollection services, IConfiguration configuration, Action<CosmosClientExtension> options)
        {
            var clientConfigure = new CosmosClientExtension();
            options(clientConfigure);

            CosmosClient client;

            if (!string.IsNullOrEmpty(clientConfigure.ConnectionString))
                client = new CosmosClient(clientConfigure.ConnectionString, clientConfigure.CosmosClientOptions);
            else if (!string.IsNullOrEmpty(clientConfigure.AccountEndpoint) && !string.IsNullOrEmpty(clientConfigure.AuthKeyOrResourceToken))
                client = new CosmosClient(clientConfigure.AccountEndpoint, clientConfigure.AuthKeyOrResourceToken, clientConfigure.CosmosClientOptions);
            else if (!string.IsNullOrEmpty(clientConfigure.AccountEndpoint) && clientConfigure.TokenCredential != null)
                client = new CosmosClient(clientConfigure.AccountEndpoint, clientConfigure.TokenCredential, clientConfigure.CosmosClientOptions);
            else
                throw new Exception("Error configuring cosmos client");

            return new CosmosDbBuilder(services, configuration, client, clientConfigure);
        }
    }
}
