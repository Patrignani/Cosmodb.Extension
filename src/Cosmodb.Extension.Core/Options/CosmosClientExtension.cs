using Azure.Core;
using Cosmodb.Extension.Core.Enums;
using Microsoft.Azure.Cosmos;

namespace Cosmodb.Extension.Core.Options
{
    public class CosmosClientExtension
    {
        public string ConnectionString { get; private set; }
        public string AccountEndpoint { get; private set; }
        public string AuthKeyOrResourceToken { get; private set; }
        public CosmosClientOptions CosmosClientOptions { get; private set; } = null;
        public TokenCredential TokenCredential { get; private set; } = null;
        public RegisterServiceType RegisterServiceType { get; private set; } = RegisterServiceType.Scoped;

        public void AddConnectionString(string connectionString) => ConnectionString = connectionString;
        public void AddAccountEndpoint(string accountEndpoint) => AccountEndpoint = accountEndpoint;
        public void AddAuthKeyOrResourceToken(string authKeyOrResourceToken) => AuthKeyOrResourceToken = authKeyOrResourceToken;
        public void AddCosmosClientOptions(CosmosClientOptions cosmosClientOptions) => CosmosClientOptions = cosmosClientOptions;
        public void AddCosmosClientOptions(TokenCredential tokenCredential) => TokenCredential = tokenCredential;
    }
}
