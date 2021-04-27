using Microsoft.Azure.Cosmos;
using System;
using System.Configuration;

namespace ATG.AzureFunctions
{
    public static class Shared
    {
        public static CosmosClient Client { get; private set; }
        static Shared()
        {
            var endpoint = Convert.ToString(Environment.GetEnvironmentVariable("loggingEndpoint"));
            var masterKey = Convert.ToString(Environment.GetEnvironmentVariable("loggingAuthKey"));
            Client = new CosmosClient(endpoint, masterKey);
        }
    }
}
